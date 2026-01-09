using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObject;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

namespace eTravelAgencija.Services.Services
{
    public class WorkApplicationService : BaseCRUDService<Model.model.WorkApplication, WorkApplicationSearchObject, Database.WorkApplication, WorkApplicationUpsertRequest, WorkApplicationUpsertRequest>, IWorkApplicationService
    {
        private readonly IConnection _rabbitConnection;
        private readonly IWebHostEnvironment _env;
        public WorkApplicationService(eTravelAgencijaDbContext context, IMapper mapper, IWebHostEnvironment env, IConnection rb) : base(context, mapper)
        {
            _env = env;
            _rabbitConnection = rb;
        }

        public override IQueryable<Database.WorkApplication> ApplyFilter(IQueryable<Database.WorkApplication> query, WorkApplicationSearchObject? search = null)
        {
            if (search?.personName != null)
            {
                var searchText = search.personName.ToLower();

                query = query.Where(x =>
                    ((x.User.FirstName ?? "").ToLower() + " " + (x.User.LastName ?? "").ToLower())
                    .Contains(searchText)
                );
            }
            return base.ApplyFilter(query, search);
        }

        public override IQueryable<WorkApplication> AddInclude(IQueryable<WorkApplication> query, WorkApplicationSearchObject? search = null)
        {
            query = query.Include(u => u.User);
            return base.AddInclude(query, search);
        }

        public async Task InsertCV(WorkApplicationUpsertRequest request)
        {
            if (request.CvFile == null || request.CvFile.Length == 0)
                throw new Exception("CV je obavezan.");

            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
            var ext = Path.GetExtension(request.CvFile.FileName).ToLower();

            if (!allowedExtensions.Contains(ext))
                throw new Exception("Dozvoljeni formati su: PDF, DOC, DOCX.");

            string root = _env.WebRootPath;
            string folder = Path.Combine(root, "cv");
            Directory.CreateDirectory(folder);

            string fileName = $"cv_{Guid.NewGuid()}{ext}";
            string fullPath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await request.CvFile.CopyToAsync(stream);
            }

            var entity = new WorkApplication
            {
                UserId = request.UserId,
                CvFileName = fileName,
                letter = request.letter,
                AppliedAt = DateTime.UtcNow
            };

            _context.WorkApplications.Add(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<CVDownloadResponse> DownloadCVAsync(int id)
        {
            var entity = await _context.WorkApplications.FindAsync(id);

            if (entity == null)
                throw new Exception("Aplikacija nije pronađena.");

            string root = Directory.GetCurrentDirectory();
            string folder = Path.Combine(root, "wwwroot", "cv");
            string fullPath = Path.Combine(folder, entity.CvFileName);


            if (!File.Exists(fullPath))
                throw new Exception("CV fajl ne postoji.");

            var response = _mapper.Map<CVDownloadResponse>(entity);

            response.fileBytes = await File.ReadAllBytesAsync(fullPath);

            return response;
        }

        public async Task InviteToInterviewAsync(int applicationId)
        {
            var application = await _context.WorkApplications
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == applicationId);

            if (application == null)
                throw new Exception("Prijava ne postoji.");

            await _context.SaveChangesAsync();

            await SendInterviewInvitationEmail(application);
        }

        private async Task SendInterviewInvitationEmail(WorkApplication app)
        {
            var channel = await _rabbitConnection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "email.interview-invitation",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = new
            {
                To = app.User.Email,
                FullName = $"{app.User.FirstName} {app.User.LastName}",
                Phone = "☎ +387 61 123 456", // agencija
                AgencyName = "eTravel",
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "email.interview-invitation",
                body: body
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Model.ResponseObject;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eTravelAgencija.Services.Services
{
    public class UserService : BaseCRUDService<Model.model.User, UserSearchObject, Database.User, UserUpsertRequest, UserUpsertRequest>, IUserService
    {
        private readonly eTravelAgencijaDbContext _context;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        public UserService(eTravelAgencijaDbContext context, IMapper mapper, UserManager<User> userManager, IConfiguration config, IUserRoleService userRoleService) : base(context, mapper)
        {
            _userManager = userManager;
            _config = config;
        }

        public override IQueryable<User> ApplyFilter(IQueryable<User> query, UserSearchObject search)
        {
            
            if (!string.IsNullOrWhiteSpace(search.personNameSearch))
            {
                var searchText = search.personNameSearch.ToLower();

                query = query.Where(x =>
                    ((x.FirstName ?? "").ToLower() + " " + (x.LastName ?? "").ToLower())
                    .Contains(searchText)
                );
            }

            return base.ApplyFilter(query, search);
        }


        public override IQueryable<User> AddInclude(IQueryable<User> query, UserSearchObject? search = null)
        {
            if (search.onlyUsers.HasValue)
            {
                query = query
                    .Include(u => u.UserRoles)
                    .Where(u => u.UserRoles.Any(r => r.RoleId == 1));
            }

            if (search.onlyWorkers.HasValue)
            {
                query = query
                    .Include(u => u.UserRoles)
                    .Where(u => u.UserRoles.Any(r => r.RoleId == 2));
            }

            if (search.onlyDirectors.HasValue)
            {
                query = query
                    .Include(u => u.UserRoles)
                    .Where(u => u.UserRoles.Any(r => r.RoleId == 3));
            }

            if (search.activeReservations.HasValue)
            {
                query = query.Include(u => u.UserReservations).Where(u => u.UserReservations.Any(r => r.IsActive == true));
            }

            return base.AddInclude(query, search);
        }

        public override async Task<Model.model.User> CreateAsync(UserUpsertRequest request)
        {

            if (await _userManager.FindByEmailAsync(request.Email) is not null)
                throw new InvalidOperationException("Korisnik sa ovom email adresom već postoji.");

            if (await _userManager.FindByNameAsync(request.Username) is not null)
                throw new InvalidOperationException("Korisnik sa ovim korisničkim imenom već postoji.");


            var user = _mapper.Map<User>(request);
            user.UserName = request.Username;
            user.CreatedAt = DateTime.UtcNow;
            user.isBlocked = false;


            var passwordValidator = new PasswordValidator<User>();
            var validationResult = await passwordValidator.ValidateAsync(_userManager, user, request.Password);

            if (!validationResult.Succeeded)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Lozinka nije validna: {errors}");
            }


            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Kreiranje korisnika nije uspjelo: {errors}");
            }


            await _userManager.AddToRoleAsync(user, "Korisnik");


            var userResponse = _mapper.Map<Model.model.User>(user);

            return userResponse;
        }


        public async Task<UserLoginResponse?> AuthenticateAsync(UserLoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null || user.isBlocked)
                return null;

            var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!validPassword)
                return null;

            return await GetUserResponseWithTokenAsync(user);
        }

        private async Task<UserLoginResponse> GetUserResponseWithTokenAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            var userResponse = _mapper.Map<UserLoginResponse>(user);
            userResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
            userResponse.Roles = roles.ToList();

            return userResponse;
        }
    }
}

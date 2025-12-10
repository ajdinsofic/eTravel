using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Model.ResponseObject;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eTravelAgencija.Services.Services
{
    public class UserService : BaseCRUDService<Model.model.User, UserSearchObject, Database.User, UserInsertRequest, UserUpdateRequest>, IUserService
    {
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

            if (search.CheckMoreRoles == true)
            {
                query = query.Where(u =>
                    u.UserRoles.Count == 1 &&
                    u.UserRoles.Any(ur => ur.RoleId == 1)
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

        public override async Task<Model.model.User> CreateAsync([FromForm] UserInsertRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not null)
                throw new InvalidOperationException("Korisnik sa ovom email adresom već postoji.");

            if (await _userManager.FindByNameAsync(request.Username) is not null)
                throw new InvalidOperationException("Korisnik sa ovim korisničkim imenom već postoji.");


            // ---------------------------
            // MAP ROLE ID → ROLE NAME
            // ---------------------------
            string roleName = request.RoleId switch
            {
                1 => "Korisnik",
                2 => "Radnik",
                3 => "Direktor",
                _ => throw new InvalidOperationException("Neispravan roleId.")
            };

            var user = _mapper.Map<User>(request);


            user.UserName = request.Username;
            user.CreatedAt = DateTime.UtcNow;
            user.isBlocked = false;
            // DUMMY Image
            user.ImageUrl = "test.jpg";

            // VALIDACIJA LOZINKE
            var passwordValidator = new PasswordValidator<User>();
            var validationResult = await passwordValidator.ValidateAsync(_userManager, user, request.Password);

            if (!validationResult.Succeeded)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Lozinka nije validna: {errors}");
            }

            // KREIRANJE KORISNIKA
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Kreiranje korisnika nije uspjelo: {errors}");
            }

            // ---------------------------
            // ADD ROLE TO USER
            // ---------------------------
            await _userManager.AddToRoleAsync(user, roleName);

            await _context.SaveChangesAsync();

            var userResponse = _mapper.Map<Model.model.User>(user);
            return userResponse;
        }

        public async Task<bool> AddUserImage(UserImageRequest request)
        {
            var user = await _context.Users.FindAsync(request.userId);

            if (user == null)
                throw new Exception("Korisnik nije pronađen.");

            if (request.Image == null || request.Image.Length == 0)
                throw new Exception("Slika nije validna.");

            // Folder: wwwroot/users
            var folderPath = Path.Combine("wwwroot", "images", "users");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Generiši ime slike
            var extension = Path.GetExtension(request.Image.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folderPath, fileName);

            // Snimi na disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            // Snimi u bazu
            user.ImageUrl = fileName;
            await _context.SaveChangesAsync();

            return true;
        }

        public override async Task BeforeUpdateAsync(User entity, UserUpdateRequest request)
        {
            // da li se username mijenja?
            if (!string.Equals(entity.UserName, request.Username, StringComparison.OrdinalIgnoreCase))
            {
                // reset security stamp → svi prethodni tokeni se automatski smatraju nevažećim
                await _userManager.UpdateSecurityStampAsync(entity);

                // ažuriraj Identity username polja
                entity.UserName = request.Username;
                entity.NormalizedUserName = request.Username.ToUpper();
            }

            // update ostalih polja normalno
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Email = request.Email;
            entity.NormalizedEmail = request.Email?.ToUpper();
            entity.PhoneNumber = request.PhoneNumber;
            entity.DateBirth = request.DateBirth;

            await base.BeforeUpdateAsync(entity, request);
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

        public async Task<bool> UnblockUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.isBlocked = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> blockUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.isBlocked = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserImageAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new Exception("User not found.");

            if (string.IsNullOrWhiteSpace(user.ImageUrl))
                return true; // nema slike

            var folderPath = Path.Combine("wwwroot", "images", "users");
            var filePath = Path.Combine(folderPath, user.ImageUrl);

            if (File.Exists(filePath))
            {
                File.Delete(filePath); // obriši iz foldera
            }

            // očisti u bazi
            user.ImageUrl = "prazno.jpg";
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CheckPasswordAsync(CheckPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
        }

        public async Task<bool> UpdateNewPasswordAsync(UpdateNewPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return false;

            // generišemo reset token
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

            return result.Succeeded;
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class UserService : IUserService
    {
        private readonly eTravelAgencijaDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserService(eTravelAgencijaDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<UserResponse>> GetAsync(UserSearchObject search)
        {

            var query = _context.Users.AsQueryable();


            if (!string.IsNullOrEmpty(search.Username))
            {
                query = query.Where(u => u.UserName.Contains(search.Username));
            }


            if (!string.IsNullOrEmpty(search.Email))
            {
                query = query.Where(u => u.Email.Contains(search.Email));
            }

            if (!string.IsNullOrEmpty(search.FTS))
            {
                string fts = search.FTS.ToLower();
                query = query.Where(u =>
                    u.UserName.ToLower().Contains(fts) ||
                    u.Email.ToLower().Contains(fts) ||
                    u.FirstName.ToLower().Contains(fts) ||
                    u.LastName.ToLower().Contains(fts));
            }

            var users = await query.ToListAsync();
            return users.Select(MapToResponse).ToList();
        }

        private UserResponse MapToResponse(User user)
        {
            return new UserResponse
            {
                Id = int.Parse(user.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                IsActive = user.EmailConfirmed,
                CreatedAt = user.CreatedAt,
                LastLoginAt = user.LastLoginAt,
                PhoneNumber = user.PhoneNumber,
                isBlocked = user.isBlocked
            };
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id.ToString());
            if (user == null)
                return null;
            return MapToResponse(user);
        }

        public async Task<UserResponse> PostAsync(UserUpsertRequest user)
        {
            
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new InvalidOperationException("A user with this email already exists.");

            if (await _context.Users.AnyAsync(u => u.UserName == user.Username))
                throw new InvalidOperationException("A user with this username already exists.");

           
            var newUser = new User
            {
                UserName = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                isBlocked = false,
                CreatedAt = DateTime.UtcNow,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"User creation failed: {errors}");
            }

           
            await _userManager.AddToRoleAsync(newUser, "Korisnik");
            return await GetUserResponseWithRolesAsync(newUser.Id);
        }

        private async Task<UserResponse> GetUserResponseWithRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new InvalidOperationException("User not found");

            var roles = await _userManager.GetRolesAsync(user);

            
            if (roles.Contains("Korisnik"))
            {
                var response = MapToResponse(user);

                response.Roles = new List<RoleResponse>
                
                {
                    new RoleResponse
                    {
                        Id = user.Id, 
                        Name = "Korisnik",
                        Description = "Osnovna korisnička rola"
                    }
                };

                return response;
            }
            
            return MapToResponse(user);
        }

        public async Task<UserResponse> PutAsync(int id, UserUpsertRequest user)
        {
            var existingUser = await _context.Users.FindAsync(id.ToString());
            if (existingUser == null)
                throw new InvalidOperationException("User not found");

            if (existingUser.Email != user.Email && await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new InvalidOperationException("A user with this email already exists.");

            if (existingUser.UserName != user.Username && await _context.Users.AnyAsync(u => u.UserName == user.Username))
                throw new InvalidOperationException("A user with this username already exists.");

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.UserName = user.Username;
            existingUser.PhoneNumber = user.PhoneNumber;

            if (!string.IsNullOrEmpty(user.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
                var passwordResult = await _userManager.ResetPasswordAsync(existingUser, token, user.Password);
                if (!passwordResult.Succeeded)
                {
                    var errors = string.Join(", ", passwordResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Password update failed: {errors}");
                }
            }

            await _context.SaveChangesAsync();
            return await GetUserResponseWithRolesAsync(existingUser.Id);
        }
    }
}

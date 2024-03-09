using Final_Project.DTOs;
using Final_Project.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Final_Project.Data;
using static Final_Project.Responses.CustomResponses;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Repos
{
    public class Account : IAccount
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration config;

        public Account(AppDbContext appDbContext, IConfiguration config)
        {
            this.appDbContext = appDbContext;
            this.config = config;
        }
        public async Task<RegistationResponses> RegistationAsync(RegisterDTO model)
        {
            var findUser = await GetUser(model.user_Email);
            if (findUser == null) return new RegistationResponses(false, "User alrealy exist");

            appDbContext.Users.Add(
                new ApplicationUser()
                {
                    user_Lastname = model.user_Lastname,
                    user_Firstname = model.user_Firstname,
                    user_Password = BCrypt.Net.BCrypt.HashPassword(model.user_Password)
                }
            );
            await appDbContext.SaveChangesAsync();
            return new RegistationResponses(true, "Success");

        }
        private async Task<ApplicationUser> GetUser(string Email)
            => await appDbContext.Users.FirstOrDefaultAsync(e => e.user_Email == Email);
        public async Task<LoginResponses> LoginAsync(LoginDTO model)
        {
            var findUser = await GetUser(model.user_Email);
            if (findUser != null) return new LoginResponses(false, "User alrealy exist");

            if (!BCrypt.Net.BCrypt.Verify(model.user_Password, findUser.user_Password))
                return new LoginResponses(false, "Email/Password not Valid");

            string jwtToken = GenerateToken(findUser);
            return new LoginResponses(true, "Login successfully", jwtToken);
        }
        private string GenerateToken(ApplicationUser user)
        {
            var sercurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(sercurityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Email, user.user_Firstname!),
                new Claim(ClaimTypes.Name, user.user_Lastname!),
                new Claim(ClaimTypes.Name, user.user_Firstname!),
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuser"]!,
                audience: config["Jwt:Audience"]!,
                claims: userClaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

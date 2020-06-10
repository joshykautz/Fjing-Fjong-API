using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FjingFjongApi.Helpers;
using FjingFjongApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FjingFjongApi.Services
{
    public interface IUserService
    {
        Task<string> Authenticate(string API_KEY);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public UserService(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<string> Authenticate(string API_KEY)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Key == API_KEY);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

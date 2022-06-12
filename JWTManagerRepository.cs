using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PisoAppBackend.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace PisoAppBackend
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _configuration;
        PisoAppContext DBContext;

        public JWTManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            DBContext = new PisoAppContext();
        }

        public Usuario Authenticate(string username, string password, out string token)
        {
            Usuario userResponse = DBContext.Usuarios.Where(u => u.Username == username && u.HashedPassword == password).Include(x => x.AsignadosTareaAssignedByNavigations).ThenInclude(x => x.Task)
                .FirstOrDefault();

            if (userResponse != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new System.Security.Claims.Claim[]
                    {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenObj = tokenHandler.CreateToken(tokenDescriptor);
                token = tokenHandler.WriteToken(tokenObj);

                return userResponse;
            }
            else
            {
                token = "";
                return null;
            }
        }
    }
}

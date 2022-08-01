using alkemy_challenge.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace alkemy_challenge.BLL
{
    public class AuthBusiness
    {
        private readonly Context _context;
        private readonly IConfiguration _config;
        public AuthBusiness(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public string GenereteToken(User user)
        {
            //get the jwt config variables
            var privateKey = _config["jwt:Key"];
            var audience = _config["jwt:Audience"];
            var issuer = _config["jwt:Issuer"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));

            //crdenciales issuer
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Role, user.Role)
                //otros claims
                //new Claim(ClaimTypes.

            };

            var token = new
                JwtSecurityToken(issuer, audience, claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddMinutes(30)); //TODO: establecerlo en config o basado en rol?

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// trivial approach to Auth User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User AuthenticateUser(string userEmail, string password)
        {
            //encontrar el usuario en el repo
            var _user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (_user != null)
            {
                return (password == _user.Password) ? _user : null;
            }
            return null;
        }
    }
}

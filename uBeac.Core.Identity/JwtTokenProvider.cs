using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace uBeac.Core.Identity
{
    public interface IJwtTokenProvider
    {
        JwtSecurityToken GenerateToken<TKey, TUser>(TUser user) where TKey : IEquatable<TKey> where TUser : User<TKey>;
    }

    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtConfig _config;
        public JwtTokenProvider(JwtConfig config)
        {
            _config = config;
        }

        public JwtSecurityToken GenerateToken<TKey, TUser>(TUser user)
            where TKey : IEquatable<TKey>
            where TUser : User<TKey>
        {

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config.Issuer,
                Audience = _config.Audience,
                Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddSeconds(_config.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            return jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        }
    }
}

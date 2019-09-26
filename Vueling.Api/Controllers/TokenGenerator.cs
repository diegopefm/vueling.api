using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using Vueling.Api.Models;

namespace Vueling.Api.Controllers
{
    internal static class TokenGenerator
    {
        public static IOptions<AppSettings> settings;

        public static string GenerateTokenJwt(string username)
        {
            var secretKey = settings.Value.jwt_secret_key;
            var audienceToken = settings.Value.jwt_audience_token;
            var issuerToken = settings.Value.jwt_issuer_token;
            var expireTime = settings.Value.jwt_expire_minutes;

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}

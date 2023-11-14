using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GoMyShops.WebAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfigurationParameters _configuration;
        public TokenService(IConfigurationParameters configuration)
        {
            _configuration = configuration;
        }


        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {

            var signingCredentials = new SigningCredentials(
                   _configuration.tokenValidationParameters.IssuerSigningKey,
                   SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration.tokenValidationParameters.ValidIssuer,
                audience: _configuration.tokenValidationParameters.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(_configuration.tokenExpiredHour ),
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = _configuration.tokenValidationParameters;

            //var tokenValidationParameters1 = new TokenValidationParameters
            //{
            //    ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
            //    ValidateIssuer = false,
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
            //    ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            //};

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}

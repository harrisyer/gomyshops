using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Azure;
using GoMyShops.Commons;
using GoMyShops.Data;
using GoMyShops.Data.Entity;
using GoMyShops.Models;
using GoMyShops.Models.WebAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace GoMyShops.BAL.WebAPI
{
    public interface ITokenServiceBAL
    {
        Task<RefreshTokenModels> Login(LoginWebApIModels input);
        Task<RefreshTokenModels> LoginFromWebApp(string userName);
        #region Definations

        #endregion
        #region Public Functions
        public Task<RefreshTokenModels> RefreshToken(string accessToken, string refreshToken);
        Task<bool> Revoke(string username);
        //string GenerateAccessToken(IEnumerable<Claim> claims);
        //string GenerateRefreshToken();
        //ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        #endregion
    }//end interface

    public class TokenServiceBAL: ITokenServiceBAL
    {
        #region Definations
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfigurationParameters _configuration;
        private readonly ILogger<AnnouncementBAL> _logger;
        IUnitOfWork _uow;
        #endregion
        #region Constructor
        public TokenServiceBAL(IConfigurationParameters configuration, ILogger<AnnouncementBAL> logger, IUnitOfWork uow, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _logger = logger;
            _uow = uow;
            _userManager = userManager;
        }

        #endregion
        #region Public Functions
        
        public async Task<RefreshTokenModels> RefreshToken(string accessToken, string refreshToken)
        {
            var returnValues = new RefreshTokenModels();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;
                if (jwtToken == null)
                {
                    return returnValues;
                }//end if

                var usernameClaim = jwtToken.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name);
                if (usernameClaim == null)
                {
                    return returnValues;
                }//end if

                var username = usernameClaim.Value;
                if (username.IsNullOrEmpty())
                {
                    return returnValues;
                }//end if
                    
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return returnValues;
                }//end if               

                if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    returnValues.ErrorMessages = "Refresh Token expires.";
                    return returnValues;
                }//end if  

                var newAccessToken = GenerateAccessToken(jwtToken.Claims);
                var newRefreshToken =GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                returnValues.IsError =await _uow.SaveAsync();

                returnValues.AccessToken = newAccessToken;
                returnValues.RefreshToken = newRefreshToken;
                return returnValues;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                returnValues.ErrorMessages = Commons.CommonSetting.PleaseContactAdmin;
                return returnValues;
            }
            finally { }
           
        }
        public async Task<bool> Revoke(string username)
        {

            try
            {
               
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) return true;

                user.RefreshToken = null;

               return await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return true;
            }
            finally { }

           
        }

        public async Task<RefreshTokenModels> LoginFromWebApp(string userName)
        {
            var returnValues = new RefreshTokenModels();
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                    return returnValues;

                else
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(
                        ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(
                       ClaimTypes.Role, RoleNames.Moderator));

                    var accessToken = GenerateAccessToken(claims);
                    //var authenticationInfo = await HttpContext.AuthenticateAsync();

                    var refreshToken = GenerateRefreshToken();

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                    returnValues.IsError = await _uow.SaveAsync();
                    returnValues.AccessToken = refreshToken;
                    returnValues.RefreshToken = accessToken;

                    return returnValues;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                returnValues.ErrorMessages = Commons.CommonSetting.PleaseContactAdmin;
                return returnValues;
            }
            finally { }
        }

        public async Task<RefreshTokenModels> Login(LoginWebApIModels input)
        {
            var returnValues = new RefreshTokenModels();
            try
            {
                var user = await _userManager.FindByNameAsync(input.UserName);
                if (user == null
                    || !await _userManager.CheckPasswordAsync(
                            user, input.Password))
                    return returnValues;

                else
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(
                        ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(
                       ClaimTypes.Role, RoleNames.Moderator));

                    var accessToken = GenerateAccessToken(claims);
                    //var authenticationInfo = await HttpContext.AuthenticateAsync();

                    var refreshToken = GenerateRefreshToken();

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                    returnValues.IsError = await _uow.SaveAsync();
                    returnValues.AccessToken = refreshToken;
                    returnValues.RefreshToken = accessToken;
                   
                    return returnValues;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                returnValues.ErrorMessages = Commons.CommonSetting.PleaseContactAdmin;
                return returnValues;
            }
            finally { }
        }
        #endregion
        #region Private Functions
        private string GenerateAccessToken(IEnumerable<Claim> claims)
    {

        var signingCredentials = new SigningCredentials(
                _configuration.tokenValidationParameters.IssuerSigningKey,
                SecurityAlgorithms.HmacSha256);

        var tokeOptions = new JwtSecurityToken(
            issuer: _configuration.tokenValidationParameters.ValidIssuer,
            audience: _configuration.tokenValidationParameters.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(_configuration.tokenExpiredHour),
            signingCredentials: signingCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
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
    #endregion
    }//end class
}//end namespace
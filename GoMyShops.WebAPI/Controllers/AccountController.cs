using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList.DTO;
using MyBGList.Models;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.ComponentModel.DataAnnotations;
using MyBGList.Attributes;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GoMyShops.Data;
using GoMyShops.Models;
using GoMyShops.WebAPI.Models;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using GoMyShops.WebAPI.Services;
using System.Linq.Dynamic.Core.Tokenizer;
using Microsoft.AspNetCore.Authentication;
using System;
using Azure.Core;
using StackExchange.Redis;
using GoMyShops.Commons;

namespace GoMyShops.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly ILogger<DomainsController> _logger;

        private readonly IConfiguration _configuration;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ITokenService _tokenService;

        public AccountController(
            DataContext context,
            ILogger<DomainsController> logger,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<ActionResult> Register(RegisterDTO input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = new ApplicationUser();
                    newUser.UserName = input.UserName;
                    newUser.Email = input.Email;
                    var result = await _userManager.CreateAsync(
                        newUser, input.Password!);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(
                            "User {userName} ({email}) has been created.",
                        newUser.UserName, newUser.Email);
                        return StatusCode(201,
                            $"User '{newUser.UserName}' has been created.");
                    }
                    else
                        throw new Exception(
                            string.Format("Error: {0}", string.Join(" ", 
                                result.Errors.Select(e => e.Description))));
                }
                else
                {
                    var details = new ValidationProblemDetails(ModelState);
                    details.Type =
                            "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    details.Status = StatusCodes.Status400BadRequest;
                    var a= new BadRequestObjectResult(details);                   
                    return new BadRequestObjectResult(details);
                }
            }
            catch (Exception e)
            {
                var exceptionDetails = new ProblemDetails();
                exceptionDetails.Detail = e.Message;
                exceptionDetails.Status =
                    StatusCodes.Status500InternalServerError;
                exceptionDetails.Type =
                        "https://tools.ietf.org/html/rfc7231#section-6.6.1";
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    exceptionDetails);
            }
        }

        [HttpPost]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<ActionResult> Login(LoginDTO input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(input.UserName);
                    if (user == null
                        || !await _userManager.CheckPasswordAsync(
                                user, input.Password))
                        return Unauthorized();
                        //throw new Exception("Invalid login attempt.");
                    else
                    {
                        //var signingCredentials = new SigningCredentials(
                        //    new SymmetricSecurityKey(
                        //        System.Text.Encoding.UTF8.GetBytes(
                        //            _configuration["JWT:SigningKey"])),
                        //    SecurityAlgorithms.HmacSha256);

                        //Check Cuurent got access token or not.
                        //var accessToken1 = await HttpContext.GetTokenAsync("access_token");
                        //if (accessToken1 != null) {
                        //    var tokenHandler = new JwtSecurityTokenHandler();

                        //    var jwtSecurityToken = tokenHandler.ReadJwtToken(accessToken1);
                        //}
                      


                        var claims = new List<Claim>();
                        claims.Add(new Claim(
                            ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(
                           ClaimTypes.Role, RoleNames.Moderator));
                        //claims.Add(new Claim(
                        //   "refresh_token", refreshToken,ClaimValueTypes.String));
                        ////claims.AddRange(
                        //    (await _userManager.GetRolesAsync(user))
                        //        .Select(r => new Claim(ClaimTypes.Role, r)));


                        var accessToken = _tokenService.GenerateAccessToken(claims);
                        //var authenticationInfo = await HttpContext.AuthenticateAsync();
                    
                        var refreshToken = _tokenService.GenerateRefreshToken();

                        // ... update tokens using refresh token flow ...

                        //authenticationInfo.Properties.UpdateTokenValue("access_token", accessToken);
                        //var aaa = authenticationInfo.Principal;

                        // authenticationInfo.Properties.SetString("refresh_token", refreshToken);
                        // authenticationInfo.Properties.ExpiresUtc = DateTime.Now.AddSeconds(300);//UpdateTokenValue("expires_at", DateTime.Now.AddSeconds(300).ToString("o"));

                        user.RefreshToken = refreshToken;
                        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                        _context.SaveChanges();
                      
                        Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                        Response.Cookies.Append("X-Username", user.UserName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                        Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

                        return Ok();

                        //return Ok(new AuthenticatedResponse
                        //{
                        //    Token = accessToken,
                        //    RefreshToken = refreshToken
                        //});

                    }
                }
                else
                {
                    var details = new ValidationProblemDetails(ModelState);
                    details.Type =
                            "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    details.Status = StatusCodes.Status400BadRequest;
                    return new BadRequestObjectResult(details);
                }
            }
            catch (Exception e)
            {
                var exceptionDetails = new ProblemDetails();
                exceptionDetails.Detail = e.Message;
                exceptionDetails.Status =
                    StatusCodes.Status401Unauthorized;
                exceptionDetails.Type =
                        "https://tools.ietf.org/html/rfc7231#section-6.6.1";
                return StatusCode(
                    StatusCodes.Status401Unauthorized,
                    exceptionDetails);
            }
        }
    }
}

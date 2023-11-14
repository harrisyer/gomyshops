using Azure.Core;
using GoMyShops.Commons;
using GoMyShops.Data;
using GoMyShops.Models;
using GoMyShops.WebAPI.Models;
using GoMyShops.WebAPI.Services;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Security.Claims;

namespace GoMyShops.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly ILogger<TokenController> _logger;

        private readonly IConfiguration _configuration;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
      
        private readonly ITokenService _tokenService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenController(DataContext context, ITokenService tokenService, ILogger<TokenController> logger, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            this._context = context;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                if (!(Request.Cookies.TryGetValue("X-Username", out var userName) && Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken)))
                    return BadRequest("Invalid Access token!");

                //if (!User.Identity.IsAuthenticated)
                //{
                //    return BadRequest("Invalid Access token!");
                //}

                Request.Cookies.TryGetValue("X-Access-Token", out var accessToken);

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;
                if (jwtToken == null)
                    return BadRequest();


                // var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
                // var username = principal.Identity.Name; //this is mapped to the Name claim by default
                var username = jwtToken.Claims.FirstOrDefault(o=> o.Type ==ClaimTypes.Name).Value;
                if (username.IsNullOrEmpty())
                    return BadRequest();
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                    return BadRequest();

                if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                    return BadRequest("Invalid client request");


                var newAccessToken = _tokenService.GenerateAccessToken(jwtToken.Claims);
                var newRefreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                _context.SaveChanges();

                Response.Cookies.Append("X-Access-Token", newAccessToken, new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });
                Response.Cookies.Append("X-Username", username, new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });
                Response.Cookies.Append("X-Refresh-Token", newRefreshToken, new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });

                return Ok();

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(string.Format("{a}, {b}", "Invalid Access",CommonSetting.PleaseContactAdmin));
            }

           
        }


       

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            try
            {
                var username = User.Identity.Name;
                //TODO harris Revoke token
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) return BadRequest();

                user.RefreshToken = null;

                _context.SaveChanges();

                return NoContent();

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(string.Format("{a}, {b}", "Revoke fail", CommonSetting.PleaseContactAdmin));
            }
           
        }
    }
}

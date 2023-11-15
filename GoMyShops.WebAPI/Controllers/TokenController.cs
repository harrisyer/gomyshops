using GoMyShops.BAL.WebAPI;
using GoMyShops.Commons;
using GoMyShops.Data;
using GoMyShops.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GoMyShops.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        #region Definations
        private readonly DataContext _context;

        private readonly ILogger<TokenController> _logger;

        private readonly IConfiguration _configuration;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ITokenServiceBAL _tokenService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructor
        public TokenController(DataContext context, ITokenServiceBAL tokenService, ILogger<TokenController> logger, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            this._context = context;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
        #region Public Functions
        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                if (!(Request.Cookies.TryGetValue("X-Username", out var userName) && Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken)))
                    return BadRequest("Invalid Access token!");

                Request.Cookies.TryGetValue("X-Access-Token", out var accessToken);
                if (accessToken == null || refreshToken == null)
                    return BadRequest();

                //var tokenHandler = new JwtSecurityTokenHandler();
                //var jwtToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;
                //if (jwtToken == null)
                //    return BadRequest();

                //var username = jwtToken.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name).Value;
                //if (username.IsNullOrEmpty())
                //    return BadRequest();
                //var user = await _userManager.FindByNameAsync(username);
                //if (user == null)
                //    return BadRequest();

                //if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                //    return BadRequest("Invalid client request");


                //var newAccessToken = _tokenService.GenerateAccessToken(jwtToken.Claims);
                //var newRefreshToken = _tokenService.GenerateRefreshToken();

                //user.RefreshToken = newRefreshToken;
                //_context.SaveChanges();

                var newtokens =await _tokenService.RefreshToken(accessToken, refreshToken);
                if (newtokens.IsError)
                {
                    return BadRequest(newtokens.ErrorMessages);
                }//end if

                Response.Cookies.Append("X-Access-Token", newtokens.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });
                Response.Cookies.Append("X-Username", userName, new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });
                Response.Cookies.Append("X-Refresh-Token", newtokens.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });

                return Ok();

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(string.Format("{a}, {b}", "Invalid Access", CommonSetting.PleaseContactAdmin));
            }//end try
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            try
            {
                var username = User.Identity.Name;
                if (username == null) return BadRequest();

                var isError = await _tokenService.Revoke(username);
                if (isError) return BadRequest();
                //var user = await _userManager.FindByNameAsync(username);
                //if (user == null) return BadRequest();

                    //user.RefreshToken = null;

                    //_context.SaveChanges();

                return NoContent();

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(string.Format("{a}, {b}", "Revoke fail", CommonSetting.PleaseContactAdmin));
            }

        }
        #endregion


    }//end class
}//end namespace
using Microsoft.IdentityModel.Tokens;

namespace GoMyShops.WebAPI.Services
{
    public interface IConfigurationParameters
    {
        public TokenValidationParameters tokenValidationParameters { get; set; }

        public int tokenExpiredHour { get; set; }

        public int refreshTokenExpiryDay { get; set; }
    }

    public class ConfigurationParameters : IConfigurationParameters
    {
      

        public TokenValidationParameters tokenValidationParameters { get; set; }

        public int tokenExpiredHour  { get; set; }

        public int refreshTokenExpiryDay { get; set; }
        
    }
}

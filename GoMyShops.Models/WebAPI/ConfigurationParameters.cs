using Microsoft.IdentityModel.Tokens;

namespace GoMyShops.Models.WebAPI
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

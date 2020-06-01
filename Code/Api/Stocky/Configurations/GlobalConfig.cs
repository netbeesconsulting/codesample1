using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Stocky.Configurations
{
    public class GlobalConfig
    {
        private IConfiguration _config;
        private IWebHostEnvironment _env;

        public GlobalConfig(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public void Register()
        {
            Common.GlobalConfig.DevConnectionString = _config["ConnectionStrings:development"];
            Common.GlobalConfig.CorsOrigin = _config["CorsOrigins"];
            Common.GlobalConfig.JwtKey = _config["JwtConfig:JwtKey"];
            Common.GlobalConfig.JwtIssuer = _config["JwtConfig:JwtIssuer"];
            Common.GlobalConfig.JwtExpireDays = _config["JwtConfig:JwtExpireDays"];
            Common.GlobalConfig.WebRootPath = _env.WebRootPath;
        }
    }
}

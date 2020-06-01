namespace Stocky.Common
{
    public static class GlobalConfig
    {
        public static string DevConnectionString { get; set; } = "Server=DESKTOP-J0HCL35;Database=Stocky;Trusted_Connection=True";
        public static string CorsOrigin { get; set; }
        public static string JwtKey { get; set; }
        public static string JwtIssuer { get; set; }
        public static string JwtExpireDays { get; set; }
        public static string WebRootPath { get; set; }
    }
}

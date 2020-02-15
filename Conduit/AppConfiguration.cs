namespace Conduit.Web
{
    public class AppConfiguration
    {
        public Connectionstrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Token Token { get; set; }
    }

    public class Connectionstrings
    {
        public string ConduitApplicationConnection { get; set; }
        public string ConduitIdentityConnection { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
    }

    public class Token
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

}

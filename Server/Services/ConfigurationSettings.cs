namespace Server.Services;

public class ConfigurationSettings
{
    private readonly IConfiguration _configuration;

    public ConfigurationSettings(IConfiguration configuration)
    {
        _configuration = configuration;

        DBConnectionString = _configuration["ConnectionStrings:DefaultConnection"];
    }

    public string DBConnectionString { get; private set; }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace eHospitalServer.DataAccess.Options;
public sealed class EmailOptiınsSetup(IConfiguration configuration) : IConfigureOptions<EmailOptions>
{
    public void Configure(EmailOptions options)
    {
        configuration.GetSection("EmailSettings").Bind(options);
    }
}


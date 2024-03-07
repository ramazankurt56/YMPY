using eHospitalServer.DataAccess.Options;
using GenericEmailService;
using Microsoft.Extensions.Options;

namespace eHospitalServer.DataAccess.Services;
public  class MailService(IOptions<EmailOptions> options)
{
    public async Task<string> SendEmailAsync(string email, string subject, string body)
    {


        EmailConfigurations configurations = new(
            Smtp: options.Value.SMTP,
            Password: options.Value.Password,
            Port: options.Value.Port,
            SSL: options.Value.SSL,
            Html: true);

        List<string> emails = new() { email };

        EmailModel<Stream> model = new(
                Configurations: configurations,
                FromEmail: options.Value.Email,
                ToEmails: emails,
                Subject: subject,
                Body: body,
                Attachments: null);

        string response = await EmailService.SendEmailWithMailKitAsync(model);
        return response;
    }
}
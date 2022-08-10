using EmailService.Handlers;
using EmailService.Interfaces;
using EmailService.Services;
using FotballersAPI.Domain.Data.Constants.RabbitMqQueues;
using FotballersAPI.Domain.Data.RabbitMqConfiguration;
using MassTransit;

namespace EmailService;
public static class DependencyInjection
{
    public static void AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentEmail(configuration["FluentEmail:FromEmail"], configuration["FluentEmail:FromName"])
            .AddRazorRenderer()
            .AddSmtpSender(configuration["FluentEmail:SmtpSender:Host"],
                 int.Parse(configuration["FluentEmail:SmtpSender:Port"]),
                           configuration["FluentEmail:SmtpSender:Username"],
                           configuration["FluentEmail:SmtpSender:Password"]);

        services.AddScoped<IEmailSenderService, EmailSenderService>();

        AddRabbitMq(services, configuration);
    }

    private static void AddRabbitMq(IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfiguration = new RabbitMqConfiguration();

        configuration.GetSection("RabbitMqConfiguration").Bind(rabbitMqConfiguration);

        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateUserConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(rabbitMqConfiguration.QueueUrl), h =>
                {
                    h.Username(rabbitMqConfiguration.Login);
                    h.Password(rabbitMqConfiguration.Password);
                });
                cfg.ReceiveEndpoint(RabbitMqQueuesConstants.ActivateUserQueue, ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry(r => r.Interval(2, 100));
                    ep.ConfigureConsumer<CreateUserConsumer>(context);
                });
            });
        });
    }
}

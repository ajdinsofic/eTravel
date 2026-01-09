using RabbitMQ.Client;
using eTravelAgencija.EmailConsumer.Configuration;
using eTravelAgencija.EmailConsumer.Consumers;
using eTravelAgencija.EmailConsumer.Services;

namespace eTravelAgencija.EmailConsumer.DependencyInjection;

public static class EmailConsumerDI
{
    public static IServiceCollection AddEmailConsumer(
        this IServiceCollection services,
        RabbitMqSettings rabbit,
        SmtpSettings smtp)
    {
        var factory = new ConnectionFactory
        {
            HostName = rabbit.Host,
            Port = rabbit.Port,
            UserName = rabbit.User,
            Password = rabbit.Password,
            VirtualHost = rabbit.VirtualHost
        };

        // ✅ JEDINA ISPRAVNA REGISTRACIJA
        services.AddSingleton<IConnection>(_ =>
            factory.CreateConnectionAsync().GetAwaiter().GetResult()
        );

        services.AddSingleton(smtp);
        services.AddSingleton<EmailSender>();
        services.AddSingleton<EmailQueueConsumer>();

        return services;
    }
}

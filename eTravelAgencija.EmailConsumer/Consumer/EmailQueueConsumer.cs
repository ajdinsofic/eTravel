using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using eTravelAgencija.EmailConsumer.Messages;
using eTravelAgencija.EmailConsumer.Services;

namespace eTravelAgencija.EmailConsumer.Consumers;

public class EmailQueueConsumer
{
    private readonly IChannel _channel;
    private readonly EmailSender _emailSender;

    public EmailQueueConsumer(
        IConnection connection,
        EmailSender emailSender)
    {
        _emailSender = emailSender;

        _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();

        // 🔐 Reset password queue
        _channel.QueueDeclareAsync(
            queue: "email.reset-password",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        ).Wait();

        // ✈️ Reservation confirmation queue
        _channel.QueueDeclareAsync(
            queue: "email.reservation-confirmation",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        ).Wait();

        _channel.BasicQosAsync(0, 1, false).Wait();
    }

    public async Task StartAsync()
    {
        // ===============================
        // RESET PASSWORD CONSUMER
        // ===============================
        var resetConsumer = new AsyncEventingBasicConsumer(_channel);

        resetConsumer.ReceivedAsync += async (sender, e) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(e.Body.ToArray());

                var message =
                    JsonSerializer.Deserialize<ResetPasswordEmailMessage>(json)
                    ?? throw new Exception("Nevalidna poruka");

                await _emailSender.SendResetPasswordEmailAsync(
                    message.To,
                    message.UserName,
                    message.NewPassword
                );

                await _channel.BasicAckAsync(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RESET EMAIL ERROR] {ex.Message}");
                await _channel.BasicNackAsync(
                    e.DeliveryTag,
                    false,
                    requeue: false
                );
            }
        };

        await _channel.BasicConsumeAsync(
            queue: "email.reset-password",
            autoAck: false,
            consumer: resetConsumer
        );

        // ===============================
        // RESERVATION CONFIRMATION CONSUMER
        // ===============================
        var reservationConsumer = new AsyncEventingBasicConsumer(_channel);

        reservationConsumer.ReceivedAsync += async (sender, e) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(e.Body.ToArray());

                var message =
                    JsonSerializer.Deserialize<ReservationConfirmationEmailMessage>(json)
                    ?? throw new Exception("Nevalidna poruka");

                await _emailSender.SendReservationConfirmationEmailAsync(message);

                await _channel.BasicAckAsync(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RESERVATION EMAIL ERROR] {ex.Message}");
                await _channel.BasicNackAsync(
                    e.DeliveryTag,
                    false,
                    requeue: false
                );
            }
        };

        await _channel.BasicConsumeAsync(
            queue: "email.reservation-confirmation",
            autoAck: false,
            consumer: reservationConsumer
        );

        // ===============================
        // INTERVIEW INVITATION CONSUMER
        // ===============================
        var interviewConsumer = new AsyncEventingBasicConsumer(_channel);

        interviewConsumer.ReceivedAsync += async (sender, e) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(e.Body.ToArray());

                var message =
                    JsonSerializer.Deserialize<InterviewInvitationEmailMessage>(json)
                    ?? throw new Exception("Nevalidna poruka");

                await _emailSender.SendInterviewInvitationAsync(message);

                await _channel.BasicAckAsync(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INTERVIEW EMAIL ERROR] {ex.Message}");
                await _channel.BasicNackAsync(
                    e.DeliveryTag,
                    false,
                    requeue: false
                );
            }
        };

        await _channel.BasicConsumeAsync(
            queue: "email.interview-invitation",
            autoAck: false,
            consumer: interviewConsumer
        );

    }
}


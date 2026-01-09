using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using eTravelAgencija.EmailConsumer.Configuration;
using eTravelAgencija.EmailConsumer.Messages;

namespace eTravelAgencija.EmailConsumer.Services;

public class EmailSender
{
    private readonly SmtpSettings _settings;

    public EmailSender(SmtpSettings settings)
    {
        _settings = settings;
    }

    public async Task SendResetPasswordEmailAsync(
    string to,
    string userName,
    string newPassword)
{
    var message = new MimeMessage();
    message.From.Add(
        new MailboxAddress(_settings.FromName, _settings.FromEmail)
    );
    message.To.Add(MailboxAddress.Parse(to));
    message.Subject = "Nova lozinka – eTravel";

    message.Body = new TextPart("html")
    {
        Text = $"""
            <h2>Zdravo {userName},</h2>

            <p>Vaša nova lozinka je:</p>

            <h3 style="color:#2c7be5">{newPassword}</h3>

            <p>
                Preporučujemo da se odmah prijavite i promijenite lozinku.
            </p>

            <br/>
            <small>eTravel tim</small>
        """
    };

    using var client = new SmtpClient();
    await client.ConnectAsync(
        _settings.Host,
        _settings.Port,
        _settings.UseSsl
            ? SecureSocketOptions.SslOnConnect
            : SecureSocketOptions.StartTls
    );

    await client.AuthenticateAsync(
        _settings.User,
        _settings.Password
    );

    await client.SendAsync(message);
    await client.DisconnectAsync(true);
}

public async Task SendReservationConfirmationEmailAsync(
    ReservationConfirmationEmailMessage data)
{
    var message = new MimeMessage();
    message.From.Add(
        new MailboxAddress(_settings.FromName, _settings.FromEmail)
    );
    message.To.Add(MailboxAddress.Parse(data.To));

    message.Subject = "Potvrda rezervacije – eTravel";

    message.Body = new TextPart("html")
    {
        Text = $"""
            <h2>Zdravo {data.UserName},</h2>

            <p>Uspješno ste rezervisali putovanje. Detalji rezervacije:</p>

            <table style="border-collapse: collapse;">
                <tr>
                    <td><b>Broj rezervacije:</b></td>
                    <td>{data.ReservationNumber}</td>
                </tr>
                <tr>
                    <td><b>Ponuda:</b></td>
                    <td>{data.OfferName}</td>
                </tr>
                <tr>
                    <td><b>Hotel:</b></td>
                    <td>{data.HotelName}</td>
                </tr>
                <tr>
                    <td><b>Period:</b></td>
                    <td>
                        {data.DepartureDate:dd.MM.yyyy} – {data.ReturnDate:dd.MM.yyyy}
                    </td>
                </tr>
                <tr>
                    <td><b>Ukupna cijena:</b></td>
                    <td>{data.TotalPrice}\$</td>
                </tr>
            </table>

            <br/>
            <p>Hvala što koristite <b>eTravel</b>.</p>

            <small>Ovo je automatska poruka.</small>
        """
    };

    using var client = new SmtpClient();
    await client.ConnectAsync(
        _settings.Host,
        _settings.Port,
        _settings.UseSsl
            ? SecureSocketOptions.SslOnConnect
            : SecureSocketOptions.StartTls
    );

    await client.AuthenticateAsync(
        _settings.User,
        _settings.Password
    );

    await client.SendAsync(message);
    await client.DisconnectAsync(true);
}

public async Task SendInterviewInvitationAsync(
    InterviewInvitationEmailMessage msg)
{
    var message = new MimeMessage();
    message.From.Add(
        new MailboxAddress(_settings.FromName, _settings.FromEmail)
    );
    message.To.Add(MailboxAddress.Parse(msg.To));
    message.Subject = "Poziv na sastanak – eTravel";

    message.Body = new TextPart("html")
    {
        Text = $"""
        <h2>Poštovani {msg.FullName},</h2>

        <p>Obavještavamo Vas da ste <b>pozvani na sastanak</b> u okviru Vaše prijave za posao.</p>

        <p>Za više informacija molimo Vas da kontaktirate našu turističku agenciju:</p>

        <p><b>{msg.AgencyName}</b><br/>
        Telefon: {msg.Phone}</p>

        <br/>
        <p>Srdačan pozdrav,<br/>
        eTravel tim</p>
        """
    };

    using var client = new SmtpClient();
    await client.ConnectAsync(
        _settings.Host,
        _settings.Port,
        SecureSocketOptions.SslOnConnect
    );

    await client.AuthenticateAsync(
        _settings.User,
        _settings.Password
    );

    await client.SendAsync(message);
    await client.DisconnectAsync(true);
}


    
}

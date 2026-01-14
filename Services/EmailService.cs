using Microsoft.Extensions.Configuration;
using ProteinStore.API.Models;
using System.Net;
using System.Net.Mail;

namespace ProteinStore.API.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        private void SendEmail(string toEmail, string subject, string body, bool isHtml)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
                throw new ArgumentException("Recipient email is empty");

            var settings = _config.GetSection("EmailSettings");

            var mail = new MailMessage
            {
                From = new MailAddress(settings["From"], "Protein Store"),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            mail.To.Add(toEmail);

            var smtp = new SmtpClient(settings["SmtpServer"])
            {
                Port = int.Parse(settings["Port"]),
                Credentials = new NetworkCredential(
                    settings["Username"],
                    settings["Password"]
                ),
                EnableSsl = true
            };

            smtp.Send(mail);
        }

        // ✅ CUSTOMER EMAIL
        public void SendOrderConfirmation(string toEmail, int orderId, decimal total)
        {
            var body = $@"
<html>
<body style='font-family:Arial'>
  <h2>Thank you for your order!</h2>
  <p><strong>Order ID:</strong> {orderId}</p>
  <p><strong>Total:</strong> ${total}</p>
  <p>Payment Method: Cash on Delivery</p>
  <hr />
  <p>Protein Store Team 💪</p>
</body>
</html>";

            SendEmail(
                toEmail,
                "Order Confirmation - Protein Store",
                body,
                true
            );
        }

        // ✅ MANAGER EMAIL
        public void SendManagerOrderNotification(Order order)
        {
            var body = $@"
New order received!

Customer: {order.CustomerName}
Email: {order.Email}
Phone: {order.Phone}
Address: {order.Address}

Total: ${order.TotalPrice}
";

            SendEmail(
                "ammarmostafa718@gmail.com",
                $"New Order #{order.Id}",
                body,
                false
            );
        }
    }
}
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
            {
                Console.WriteLine("❌ Email failed: TO email is empty");
                return;
            }

            var from = _config["EmailSettings:From"];
            var smtpServer = _config["EmailSettings:SmtpServer"];
            var port = _config["EmailSettings:Port"];
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];

            if (string.IsNullOrWhiteSpace(from) ||
                string.IsNullOrWhiteSpace(smtpServer) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("❌ Email config missing. Check Render ENV variables.");
                return;
            }

            var mail = new MailMessage
            {
                From = new MailAddress(from, "Protein Store"),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            mail.To.Add(toEmail);

            var smtp = new SmtpClient(smtpServer)
            {
                Port = int.Parse(port),
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            smtp.Send(mail);

            Console.WriteLine($"✅ Email sent to {toEmail}");
        }

        public void SendOrderConfirmation(string toEmail, int orderId, decimal total)
        {
            var body = $@"
<h2>Thank you for your order 💪</h2>
<p><strong>Order ID:</strong> {orderId}</p>
<p><strong>Total:</strong> ${total}</p>
<p>Payment Method: Cash on Delivery</p>
<p>Protein Store Team</p>";

            SendEmail(
                toEmail,
                "Order Confirmation - Protein Store",
                body,
                true
            );
        }

        public void SendManagerOrderNotification(Order order)
        {
            var managerEmail = _config["EmailSettings:ManagerEmail"];

            var body = $@"
New order received!

Customer: {order.CustomerName}
Email: {order.Email}
Phone: {order.Phone}
Address: {order.Address}

Total: ${order.TotalPrice}
";

            SendEmail(
                managerEmail,
                $"New Order #{order.Id}",
                body,
                false
            );
        }
    }
}
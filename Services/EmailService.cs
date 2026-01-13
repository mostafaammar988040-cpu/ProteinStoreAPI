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

        // 🔹 CORE EMAIL METHOD (used by all emails)
        private void SendEmail(string toEmail, string subject, string body, bool isHtml)
        {
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

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email failed: " + ex.Message);
            }
        }

        // 🔹 CUSTOMER ORDER CONFIRMATION EMAIL
        public void SendOrderConfirmation(string toEmail, int orderId, decimal total)
        {
            var body = $@"
<html>
  <body style='font-family: Arial, sans-serif; color:#020617'>
    <h2 style='color:#2563eb'>Thank you for your order!</h2>
    <p><strong>Order ID:</strong> {orderId}</p>
    <p><strong>Total:</strong> ${total}</p>
    <p><strong>Payment Method:</strong> Cash on Delivery</p>
    <hr />
    <p>We will contact you soon to confirm delivery.</p>
    <p><strong>Protein Store Team 💪</strong></p>
  </body>
</html>";

            SendEmail(
                toEmail,
                "Order Confirmation - Protein Store",
                body,
                isHtml: true
            );
        }

        // 🔹 MANAGER ORDER NOTIFICATION EMAIL
        public void SendManagerOrderNotification(Order order)
        {
            var body = $@"
New order received!

Customer: {order.CustomerName}
Email: {order.Email}
Phone: {order.Phone}
Address: {order.Address}

Total: ${order.TotalPrice}

Items:
{string.Join("\n", order.OrderItems.Select(i =>
    $"- {i.Product.Name} x{i.Quantity}"
))}

Please prepare the order.
";

            SendEmail(
                "ammarmostafa718@gmail.com", // 👈 manager email (can move to config later)
                $"🧾 New Order #{order.Id}",
                body,
                isHtml: false
            );
        }
    }
}

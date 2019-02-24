using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;


namespace OnlineShoppingStore.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new System.Net.NetworkCredential(emailSettings.UserName, emailSettings.Password);

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("-----")
                    .AppendLine("Items:");

                foreach (var item in cart.Lines)
                {
                    var subtotal = item.Product.Price * item.Quantity;
                    body.AppendFormat("{0} X {1} (Subtotal: {2:C})\n",
                        item.Quantity,
                        item.Product.Name,
                        subtotal);
                }
                body.AppendFormat("Total order value: {0:C}",
                    cart.ComputeTotalValue())
                    .AppendLine("----")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? " ")
                    .AppendLine(shippingInfo.Line3 ?? " ")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("----")
                    .AppendFormat("Gift Wrap: {0}",
                    shippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "New order submitted.",
                    body.ToString());

                smtpClient.Send(mailMessage);
            }
        }
    }
}

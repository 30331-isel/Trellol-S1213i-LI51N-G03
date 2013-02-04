using EntitiesLogic.Entities;
using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Security
{
    public class SecurityUtils
    {
        public static string EncodePassword(string password)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(password);
            encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }

        public static void SendConfirmationEmail(string host, int port, User user, string emailFrom)
        {
            String msg = String.Format(
                "Para activar o user clique em http://{0}:{1}/Account/Validate?name={2}&passHash={3}", 
                host, 
                port, 
                user.Username, 
                user.Password.GetHashCode());

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("admin", "admin");
            MailMessage message = new MailMessage();
            message.To.Add(user.Email);
            message.From = new MailAddress(emailFrom/*"trellol@gmail.com"*/);
            message.Subject = "[No reply] Serviço automático de validação";
            message.Body = msg;
            client.Send(message);
        }
    }
}

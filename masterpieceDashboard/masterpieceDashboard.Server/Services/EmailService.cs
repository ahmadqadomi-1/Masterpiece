using System;
using System.Net;
using System.Net.Mail;

namespace masterpieceDashboard.Server.Services
{
    public class EmailService
    {
        // Method to send email
        public static void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                // معلومات الإرسال
                var fromAddress = new MailAddress("ahmad.alqadomi02@gmail.com", "شركة القدومي للسيراميك");
                var toAddress = new MailAddress(toEmail);

                // استخدم كلمة مرور التطبيق هنا
                const string fromPassword = "jcow swrz jnqf fjki";  // كلمة مرور التطبيق التي حصلت عليها

                // إعدادات SMTP الخاصة بـ Gmail
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",  // تأكد من استخدام SMTP الخاص بـ Gmail
                    Port = 587,               // المنفذ الصحيح لـ TLS
                    EnableSsl = true,         // تأكد من تفعيل تشفير SSL
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)  // استخدم كلمة مرور التطبيق هنا
                };

                // رسالة البريد الإلكتروني
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };

                // إرسال الرسالة
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                // في حالة حدوث خطأ، نقوم بطباعة رسالة الخطأ
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}

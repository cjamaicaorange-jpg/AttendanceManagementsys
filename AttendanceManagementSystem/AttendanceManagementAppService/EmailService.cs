using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace AttendanceManagementAppService
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Sends a notification email for any attendance action (Add, Update, Delete).
        /// </summary>
        /// <param name="action">The action performed: "Added", "Updated", or "Deleted"</param>
        /// <param name="studentName">The student's name</param>
        /// <param name="day">The attendance day</param>
        /// <param name="status">The attendance status ("p" or "a")</param>
        public void SendAttendanceNotification(string action, string studentName, string day, string status)
        {
            try
            {
                string statusText = status.ToLower() == "p" ? "Present"
                                  : status.ToLower() == "a" ? "Absent"
                                  : status;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(
                    _configuration["EmailSettings:FromName"],
                    _configuration["EmailSettings:FromEmail"]));
                message.To.Add(new MailboxAddress(
                    "Admin",
                    _configuration["EmailSettings:ToEmail"]));

                message.Subject = $"Attendance {action} – {studentName}";

                message.Body = new TextPart("plain")
                {
                    Text = $"Attendance Record {action}\n" +
                           $"{"".PadRight(30, '-')}\n" +
                           $"Student : {studentName}\n" +
                           $"Day     : {day}\n" +
                           $"Status  : {statusText}\n\n" +
                           $"This is an automated notification from the Attendance Management System."
                };

                using var client = new SmtpClient();
                client.Connect(
                    _configuration["EmailSettings:SmtpHost"],
                    int.Parse(_configuration["EmailSettings:SmtpPort"]!),
                    SecureSocketOptions.StartTls);

                client.Authenticate(
                    _configuration["EmailSettings:Username"],
                    _configuration["EmailSettings:Password"]);

                client.Send(message);
                client.Disconnect(true);

                Console.WriteLine($"[Email] Notification sent for {action.ToLower()} of {studentName}.");
            }
            catch (Exception ex)
            {
                // Don't crash the app if email fails — just log it
                Console.WriteLine($"[Email] Failed to send notification: {ex.Message}");
            }
        }
    }
}

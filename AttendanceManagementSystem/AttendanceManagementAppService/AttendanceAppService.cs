using AttendanceManagementDataService;
using AttendanceManagementModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace AttendanceManagementAppService
{
    public class AttendanceAppService
    {
        private readonly AttendanceDataService attendancedataservice =
            new AttendanceDataService(new AttendanceManagementDBData());

        private readonly EmailService emailService;

        public AttendanceAppService(IConfiguration configuration)
        {
            emailService = new EmailService(configuration);
        }

        public void AddRecord(string studName, string date, string status)
        {
            if (!IsValidStatus(status))
            {
                Console.WriteLine("Invalid status. Use 'p' for Present or 'a' for Absent.");
                return;
            }

            var record = new AttendanceItems
            {
                StudentName = studName,
                Day = date,
                Status = status.ToLower()
            };

            attendancedataservice.Add(record);
            emailService.SendAttendanceNotification("Added", studName, date, status);
        }

        public void UpdateRecord(string name, string day, string status)
        {
            if (!IsValidStatus(status))
            {
                Console.WriteLine("Invalid status. Use 'p' for Present or 'a' for Absent.");
                return;
            }

            attendancedataservice.Update(name, day, status.ToLower());
            emailService.SendAttendanceNotification("Updated", name, day, status);
        }

        public void DeleteRecord(string name)
        {
            // Fetch the record before deleting so we can include day/status in the email
            var records = attendancedataservice.GetAttendance();
            var existing = records.Find(x => x.StudentName == name);

            attendancedataservice.Delete(name);

            string day = existing?.Day ?? "N/A";
            string status = existing?.Status ?? "N/A";
            emailService.SendAttendanceNotification("Deleted", name, day, status);
        }

        public List<AttendanceItems> GetAttendance()
        {
            return attendancedataservice.GetAttendance();
        }

        private bool IsValidStatus(string status)
        {
            var s = status.ToLower();
            return s == "p" || s == "a";
        }
    }
}

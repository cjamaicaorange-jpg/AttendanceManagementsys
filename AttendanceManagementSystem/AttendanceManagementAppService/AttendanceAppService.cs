using AttendanceManagementDataService;
using AttendanceManagementModels;
using System;
using System.Collections.Generic;

namespace AttendanceManagementAppService
{
    public class AttendanceAppService
    {
        private readonly AttendanceDataService attendancedataservice =
            new AttendanceDataService(new AttendanceManagementDBData());

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
        }

        public void UpdateRecord(string name, string day, string status)
        {
            if (!IsValidStatus(status))
            {
                Console.WriteLine("Invalid status. Use 'p' for Present or 'a' for Absent.");
                return;
            }

            attendancedataservice.Update(name, day, status.ToLower());
        }

        public void DeleteRecord(string name)
        {
            attendancedataservice.Delete(name);
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

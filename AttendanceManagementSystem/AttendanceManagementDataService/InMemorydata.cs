using System.Collections.Generic;
using AttendanceManagementModels;

namespace AttendanceManagementDataService
{
    // In-memory implementation of IAttendance — useful for testing without a DB or JSON file
    public class InMemorydata : IAttendance
    {
        private readonly List<AttendanceItems> records = new List<AttendanceItems>();

        public void Add(AttendanceItems record)
        {
            records.Add(record);
        }

        public List<AttendanceItems> GetAttendance()
        {
            return records;
        }

        public bool checkStatus(string status)
        {
            return records.Exists(x => x.Status == status);
        }

        public void Update(string name, string day, string status)
        {
            var record = records.Find(x => x.StudentName == name);
            if (record != null)
            {
                record.Day = day;
                record.Status = status;
            }
        }

        public void Delete(string studentName)
        {
            var record = records.Find(x => x.StudentName == studentName);
            if (record != null)
                records.Remove(record);
        }
    }
}

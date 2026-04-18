using System.Collections.Generic;
using AttendanceManagementModels;
namespace AttendanceManagementDataService
{
    public class InMemorydata
    {
        public List<AttendanceItems> records = new List<AttendanceItems>();

        public void AddAttendance(AttendanceItems record)
        {
            records.Add(record);
        }
        public void UpdateRecord(AttendanceItems record)
        {
            records.Add(record);
        }

        public List<AttendanceItems> GetAttendance()
        {
            return records;
        }

    }
    
}

using System.Collections.Generic;
using AttendanceManagementModels;
namespace AttendanceManagementDataService
{
    public class AttendanceDataService
    {
        public List<AttendanceItems> records = new List<AttendanceItems>();

        public void AddAttendance(AttendanceItems record)
        {
            records.Add(record);
        }

        public List<AttendanceItems> GetAttendance()
        {
            return records;
        }

    }
    
}

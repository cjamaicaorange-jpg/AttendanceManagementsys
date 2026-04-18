using AttendanceManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagementDataService
{
    public class AttendanceDataService

    {
        private List<AttendanceItems> attendanceList = new List<AttendanceItems>();

        IAttendance dataService;
        public AttendanceDataService(IAttendance attendancedataService)
        {
            dataService = attendancedataService;
        }
        public void Add(AttendanceItems attendanceitems)
        {
            dataService.Add(attendanceitems);
        }
        public List<AttendanceItems> GetAttendance()
        {
            return dataService.GetAttendance();
        }
        public bool checkStatus(string status)
        {
            return dataService.checkStatus(status);
        }
        public void Update(AttendanceItems item)
        {
            var record = attendanceList.Find(x => x.StudentName == item.StudentName);
            if (record != null)
            {
                record.Day = item.Day;
                record.Status = item.Status;
            }
        }
        public void Delete(string studentName)
        {
            var record = attendanceList.Find(x => x.StudentName == studentName);
            if (record != null)
                attendanceList.Remove(record);
        }
    }
}

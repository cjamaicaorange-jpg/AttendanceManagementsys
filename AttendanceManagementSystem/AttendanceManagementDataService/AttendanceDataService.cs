using AttendanceManagementModels;
using System.Collections.Generic;

namespace AttendanceManagementDataService
{
    public class AttendanceDataService
    {
        private readonly IAttendance dataService;

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

        public void Update(string name, string day, string status)
        {
            dataService.Update(name, day, status);
        }

        public void Delete(string studentName)
        {
            dataService.Delete(studentName);
        }
    }
}

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
    }
}

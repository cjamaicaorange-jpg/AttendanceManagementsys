using AttendanceManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagementDataService
{
    public interface IAttendance
    {
        void Add(AttendanceItems attendanceitems);
        List<AttendanceItems> GetAttendance();
        bool checkStatus(string status);
    }
}

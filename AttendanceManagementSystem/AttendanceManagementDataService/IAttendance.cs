using AttendanceManagementModels;
using System.Collections.Generic;

namespace AttendanceManagementDataService
{
    public interface IAttendance
    {
        void Add(AttendanceItems attendanceitems);
        List<AttendanceItems> GetAttendance();
        bool checkStatus(string status);
        void Update(string name, string day, string status);
        void Delete(string studentName);
    }
}

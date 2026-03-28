using AttendanceManagementDataService;
using AttendanceManagementModels;
namespace AttendanceManagementAppService
{
    public class AttendanceAppService
    {
        //  InMemorydata attendancedataservice = new InMemorydata();
        AttendanceDataService attendancedataservice = new AttendanceDataService(new AttendanceManagementDBData());
        AttendanceJSONData attendanceJsonData = new AttendanceJSONData(); 

        public void AddRecord(string studName, string date, string status) {
            
            if (!checkStatus(status))
            {
                Console.WriteLine("Invalid status.");
                return;
            }

            var record = new AttendanceItems
            {
                StudentName = studName,
                Day = date,
                Status = status
            };
            //   attendancedataservice.AddAttendance(record);
            attendancedataservice.Add(record);
            attendanceJsonData.Add(record);

        }
        public List<AttendanceItems> GetAttendance()
        {
            return attendancedataservice.GetAttendance();
        }
        bool checkStatus(string status)
        {
            status = status.ToLower();

            if (status == "p" || status == "a")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


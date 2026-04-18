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
        public void UpdateRecord(string name, string day, string status)
        {
            var attendanceList = attendancedataservice.GetAttendance();

            var record = attendanceList.Find(x => x.StudentName == name);

            if (record != null)
            {
                record.Day = day;
                record.Status = status;
            }
            else
            {
                Console.WriteLine("Record not found.");
            }
        }

        public void DeleteRecord(string name)
{
            var attendanceList = attendancedataservice.GetAttendance();

            var record = attendanceList.Find(x => x.StudentName == name);

            if (record != null)
            {
                attendanceList.Remove(record);
                Console.Write("Record deleted.");
            }
            else
            {
                Console.WriteLine("Record not found.");
            }
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


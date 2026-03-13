using System;
using AttendanceManagementAppService;
using AttendanceManagementModels;

namespace AttendanceManagementSystem
{
    class Program
    {
        static AttendanceAppService attendanceAppService = new AttendanceAppService();

        static void Main(string[] args)
        {
            Console.WriteLine("ATTENDANCE MANAGEMENT SYSTEM");

         

            while (true)
            {
                ShowMenu();

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RecordAttendance();
                        break;

                    case "2":
                        ViewAttendance();
                        break;

                    case "3":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n------ MENU ------");
            Console.WriteLine("[1] Record Attendance");
            Console.WriteLine("[2] View Attendance");
            Console.WriteLine("[3] Exit");
            Console.Write("Choose option: ");
        }

        static void RecordAttendance()
        {
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Day: ");
            string day = Console.ReadLine();

            Console.Write("Enter Status (p = present, a = absent): ");
            string status = Console.ReadLine();

            attendanceAppService.AddRecord(name, day, status);

            Console.WriteLine("Attendance recorded successfully.");
        }

        static void ViewAttendance()
        {
            var records = attendanceAppService.GetAttendance();

            Console.WriteLine("\n------ ATTENDANCE RECORDS ------");

            if (records.Count == 0)
            {
                Console.WriteLine("No records found.");
                return;
            }

            foreach (AttendanceItems item in records)
            {
                string statusText = "";

                if (item.Status.ToLower() == "p")
                    statusText = "Present";
                else if (item.Status.ToLower() == "a")
                    statusText = "Absent";

                Console.WriteLine($"Student: {item.StudentName} | Day: {item.Day} | Status: {statusText}");
            }
        }
    }
}
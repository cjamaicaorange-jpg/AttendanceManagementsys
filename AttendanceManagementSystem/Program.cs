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
                        EditAttendance();
                        break;

                    case "4":
                        DeleteAttendance();
                        break;

                    case "5":
                        AttendanceReports();
                        break;

                    case "6":
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
            Console.WriteLine("[3] Edit Attendance");
            Console.WriteLine("[4] Delete Attendance");
            Console.WriteLine("[5] Attendance Reports");
            Console.WriteLine("[6] Exit");
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

        static void EditAttendance()
        {
            ViewAttendance();

            Console.Write("\nEnter Student Name to edit: ");
            string name = Console.ReadLine();

            Console.Write("Enter new Day: ");
            string newDay = Console.ReadLine();

            Console.Write("Enter new Status (p = present, a = absent): ");
            string newStatus = Console.ReadLine();

            attendanceAppService.UpdateRecord(name, newDay, newStatus);

            Console.WriteLine("Attendance updated successfully.");
        }
        static void DeleteAttendance()
        {
            ViewAttendance();

            Console.Write("\nEnter Student Name to delete: ");
            string name = Console.ReadLine();

            attendanceAppService.DeleteRecord(name);

            Console.WriteLine("Attendance record deleted successfully.");
        }

        static void AttendanceReports()
        {
            var records = attendanceAppService.GetAttendance();

            int totalPresent = 0;
            int totalAbsent = 0;

            foreach (AttendanceItems item in records)
            {
                if (item.Status.ToLower() == "p")
                    totalPresent++;

                else if (item.Status.ToLower() == "a")
                    totalAbsent++;
            }

            Console.WriteLine("\n------ ATTENDANCE REPORT ------");
            Console.WriteLine($"Total Present: {totalPresent}");
            Console.WriteLine($"Total Absent: {totalAbsent}");
        }
    }
}
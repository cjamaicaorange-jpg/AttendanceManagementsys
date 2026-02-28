using System;

public class AttendanceManagementSystem
{
    static public void Main()
    {
        Console.WriteLine("ATTENDANCE MANAGEMENT SYSTEM");

        string[] studentname = { "rapsing", "dela cruz", "tolarba", "malang", "casallos" };
        string[] day = { "monday", "tuesday", "wednesday", "thursday", "saturday" };

        char[,] attendance = new char[5, 4];
        

        string[] day = { "monday","tuesday", "wednesday", "thursday", "saturday" };

        char[,] attendance = new char[5, 4];

        while (true)
        {
            Console.WriteLine("\n1. Record Attendance");
            Console.WriteLine("2. View Record");
            Console.WriteLine("3. Exit");
            Console.Write("Choose: ");

            int choose = Convert.ToInt32(Console.ReadLine());

            switch (choose)
            {
                case 1:
                    Console.Write("Enter student name: ");
                    string studentInput = Console.ReadLine().ToLower();

                    Console.Write("Enter day: ");
                    string dayInput = Console.ReadLine().ToLower();

                    int studentIndex = Array.IndexOf(studentname, studentInput);
                    int dayIndex = Array.IndexOf(day, dayInput);

                    if (studentIndex == -1 || dayIndex == -1)
                    {
                        Console.WriteLine("Invalid student name or day.");
                        break;
                    }

                    Console.Write("Type 'p' for present or 'a' for absent: ");
                    char status = Console.ReadLine().ToLower()[0];

                    if (status == 'p' || status == 'a')
                    {
                        attendance[studentIndex, dayIndex] = status;
                        Console.WriteLine("Attendance Recorded Successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid status input.");
                    }

                    break;

                case 2:
                    Console.Write("Enter student name to view record: ");
                    string viewStudent = Console.ReadLine().ToLower();

                    int viewIndex = Array.IndexOf(studentname, viewStudent);

                    if (viewIndex == -1)
                    {
                        Console.WriteLine("Student not found.");
                        break;
                    }

                    Console.WriteLine("\nAttendance record for " + studentname[viewIndex] + ":");

                    for (int j = 0; j < day.Length; j++)
                    {
                        if (attendance[viewIndex, j] == 'p')
                        {
                            Console.WriteLine(day[j] + " - PRESENT");
                        }
                        else if (attendance[viewIndex, j] == 'a')
                        {
                            Console.WriteLine(day[j] + " - ABSENT");
                        }
                        else
                        {
                            Console.WriteLine(day[j] + " - No record");
                        }
                    }

                    break;

                case 3:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
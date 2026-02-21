using System.Data.Common;

namespace AttendanceManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ATTENDACE MANAGEMENT SYSTEM");

            Console.WriteLine("Choices: Record Report View");
            Console.WriteLine("Enter choice: ");
            String inputchoice = Console.ReadLine();

            if (inputchoice == "record")
            {
                Console.WriteLine("You are Present.");
            }
            else if (inputchoice == "report")
            {
                Console.WriteLine("Choices: File for Absent\nExit");
            }

        }
    }
}
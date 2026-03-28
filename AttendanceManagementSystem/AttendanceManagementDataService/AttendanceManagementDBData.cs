using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceManagementModels;
using AttendanceManagementDataService;

namespace AttendanceManagementModels
{
    public class AttendanceManagementDBData : IAttendance
    {
        private string connectionString
            = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = db_Attendance; Integrated Security = True; TrustServerCertificate=True;";

        private SqlConnection sqlConnection;

        public AttendanceManagementDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
            AddSeeds();
        }
        private void AddSeeds()
        {
            var existing = GetAttendance();
            if (existing.Count == 0)
            {
                AttendanceItems attendanceCheck = new AttendanceItems
                {
                    StudentName = "Rubie",
                    Day = "Saturday",
                    Status = "Absent"
                };
                Add(attendanceCheck);
            }
        }

        public void Add(AttendanceItems attend)
        {
            var insertStatement = "INSERT INTO tbl_Attendance VALUES (@StudentName,@Day,@Status)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@StudentName", attend.StudentName);
            insertCommand.Parameters.AddWithValue("@Day", attend.Day);
            insertCommand.Parameters.AddWithValue("@Status", attend.Status);
            sqlConnection.Open();

            insertCommand.ExecuteNonQuery();

            sqlConnection.Close();

        }

        public List<AttendanceItems> GetAttendance()
        {
            string selectStatement = "SELECT StudentName, Day, Status FROM tbl_Attendance";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            var attendanceitems = new List<AttendanceItems>();
            while (reader.Read())
            {
                AttendanceItems Att = new AttendanceItems();
                Att.StudentName = reader["StudentName"].ToString();
                Att.Day = reader["Day"].ToString();
                Att.Status = reader["Status"].ToString();

                attendanceitems.Add(Att);
            }
            sqlConnection.Close();
            return attendanceitems;
        }
        public bool checkStatus(string status)
        {
            var selectStatement = "SELECT StudentName,Day,Status FROM tbl_Attendance WHERE Status = @Status";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@Status", status);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var attendanceitems = new AttendanceItems();

            while (reader.Read())
            {
                attendanceitems.StudentName = reader["StudentName"].ToString();
                attendanceitems.Day = reader["Day"].ToString();
                attendanceitems.Status = reader["Status"].ToString();
            }
            sqlConnection.Close();
            return attendanceitems.Status != null;
        }
    }
}


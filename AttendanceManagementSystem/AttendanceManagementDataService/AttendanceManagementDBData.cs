using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using AttendanceManagementModels;
using AttendanceManagementDataService;

namespace AttendanceManagementModels
{
    public class AttendanceManagementDBData : IAttendance
    {
        private string connectionString
            = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=db_Attendance; Integrated Security=True; TrustServerCertificate=True;";

        public AttendanceManagementDBData()
        {
            AddSeeds();
        }

        private void AddSeeds()
        {
            var existing = GetAttendance();
            if (existing.Count == 0)
            {
                Add(new AttendanceItems
                {
                    StudentName = "Rubie",
                    Day = "Saturday",
                    Status = "a"
                });
            }
        }

        public void Add(AttendanceItems attend)
        {
            var insertStatement = "INSERT INTO tbl_Attendance (StudentName, Day, Status) VALUES (@StudentName, @Day, @Status)";
            using var connection = new SqlConnection(connectionString);
            using var insertCommand = new SqlCommand(insertStatement, connection);

            insertCommand.Parameters.AddWithValue("@StudentName", attend.StudentName);
            insertCommand.Parameters.AddWithValue("@Day", attend.Day);
            insertCommand.Parameters.AddWithValue("@Status", attend.Status);

            connection.Open();
            insertCommand.ExecuteNonQuery();
        }

        public List<AttendanceItems> GetAttendance()
        {
            string selectStatement = "SELECT StudentName, Day, Status FROM tbl_Attendance";
            using var connection = new SqlConnection(connectionString);
            using var selectCommand = new SqlCommand(selectStatement, connection);

            connection.Open();
            using var reader = selectCommand.ExecuteReader();

            var attendanceitems = new List<AttendanceItems>();
            while (reader.Read())
            {
                attendanceitems.Add(new AttendanceItems
                {
                    StudentName = reader["StudentName"].ToString() ?? string.Empty,
                    Day = reader["Day"].ToString() ?? string.Empty,
                    Status = reader["Status"].ToString() ?? string.Empty
                });
            }
            return attendanceitems;
        }

        public void Update(string name, string day, string status)
        {
            string updateStatement = "UPDATE tbl_Attendance SET Day = @Day, Status = @Status WHERE StudentName = @Name";
            using var connection = new SqlConnection(connectionString);
            using var updateCommand = new SqlCommand(updateStatement, connection);

            updateCommand.Parameters.AddWithValue("@Name", name);
            updateCommand.Parameters.AddWithValue("@Day", day);
            updateCommand.Parameters.AddWithValue("@Status", status);

            connection.Open();
            updateCommand.ExecuteNonQuery();
        }

        public void Delete(string studentName)
        {
            string deleteStatement = "DELETE FROM tbl_Attendance WHERE StudentName = @Name";
            using var connection = new SqlConnection(connectionString);
            using var deleteCommand = new SqlCommand(deleteStatement, connection);

            deleteCommand.Parameters.AddWithValue("@Name", studentName);

            connection.Open();
            deleteCommand.ExecuteNonQuery();
        }

        public bool checkStatus(string status)
        {
            var selectStatement = "SELECT COUNT(*) FROM tbl_Attendance WHERE Status = @Status";
            using var connection = new SqlConnection(connectionString);
            using var selectCommand = new SqlCommand(selectStatement, connection);

            selectCommand.Parameters.AddWithValue("@Status", status);

            connection.Open();
            int count = (int)selectCommand.ExecuteScalar();
            return count > 0;
        }
    }
}

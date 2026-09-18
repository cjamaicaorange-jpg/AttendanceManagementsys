using AttendanceManagementModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AttendanceManagementDataService
{
    public class AttendanceJSONData : IAttendance
    {
        private List<AttendanceItems> atten = new List<AttendanceItems>();
        private readonly string _jsonFileName;

        public AttendanceJSONData()
        {
            _jsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AttendanceJSONData.json");
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            if (File.Exists(_jsonFileName))
            {
                RetrieveDataFromJsonFile();
            }

            if (atten.Count == 0)
            {
                atten.Add(new AttendanceItems { StudentName = "Rubie", Day = "Saturday", Status = "a" });
                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            // Use File.Create to overwrite fully, preventing leftover bytes from old content
            using var outputStream = File.Create(_jsonFileName);
            JsonSerializer.Serialize(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions { Indented = true }),
                atten);
        }

        private void RetrieveDataFromJsonFile()
        {
            var json = File.ReadAllText(_jsonFileName);
            atten = JsonSerializer.Deserialize<List<AttendanceItems>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<AttendanceItems>();
        }

        public void Add(AttendanceItems attends)
        {
            RetrieveDataFromJsonFile();
            atten.Add(attends);
            SaveDataToJsonFile();
        }

        public List<AttendanceItems> GetAttendance()
        {
            RetrieveDataFromJsonFile();
            return atten;
        }

        public void Update(string name, string day, string status)
        {
            RetrieveDataFromJsonFile();
            var record = atten.Find(x => x.StudentName == name);
            if (record != null)
            {
                record.Day = day;
                record.Status = status;
                SaveDataToJsonFile();
            }
        }

        public void Delete(string studentName)
        {
            RetrieveDataFromJsonFile();
            var record = atten.Find(x => x.StudentName == studentName);
            if (record != null)
            {
                atten.Remove(record);
                SaveDataToJsonFile();
            }
        }

        public bool checkStatus(string status)
        {
            RetrieveDataFromJsonFile();
            return atten.Any(x => x.Status == status);
        }
    }
}

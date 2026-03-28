using AttendanceManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AttendanceManagementDataService
{
    public class AttendanceJSONData : IAttendance
    {
        private List<AttendanceItems> atten = new List<AttendanceItems>();

        private string _jsonFileName;

        public AttendanceJSONData()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/AttendanceJSONData.json";
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile();

            if (atten.Count == 0)
            {
                atten.Add(new AttendanceItems { StudentName = "Rubie", Day = "Saturday", Status = "Absent" });
                SaveDataToJsonFile();
            }
          
        }
        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize<List<AttendanceItems>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , atten);
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            using (var jsonFileReader = File.OpenText(this._jsonFileName))
            {
                this.atten = JsonSerializer.Deserialize<List<AttendanceItems>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }

        public void Add(AttendanceItems attends)
        {
            atten.Add(attends);
            SaveDataToJsonFile();
        }

        public List<AttendanceItems> GetAttendance()
        {
            RetrieveDataFromJsonFile();
            return atten;
        }

        public bool checkStatus(string status)
        {
            RetrieveDataFromJsonFile();
            return atten.Where(x => x.Status == status).Any();
        }


    }
}

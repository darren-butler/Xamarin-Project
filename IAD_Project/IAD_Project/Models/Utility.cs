using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace IAD_Project.Models
{
    public static class Utility
    {
        public static Course DeserializeCourse()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine(path, "CourseData.txt");
            var data = File.ReadAllText(filename);
            var course = JsonConvert.DeserializeObject<Course>(data);

            return course;

        }// DeserializeCourse()

        public static void DeleteCourse()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine(path, "CourseData.txt");
            File.Delete(filename);
        }

    }// Utility

}

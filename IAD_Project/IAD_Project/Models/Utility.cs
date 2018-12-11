using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;


namespace IAD_Project.Models
{
    static class Utility
    {
        public static Course DeserializeCourse()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine(path, "CourseData.txt");
            var data = File.ReadAllText(filename);
            var course = JsonConvert.DeserializeObject<Course>(data);

            return course;

        }// DeserializeCourse()

    }// Utility

}

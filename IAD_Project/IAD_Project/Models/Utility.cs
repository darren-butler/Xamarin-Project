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

        // Deep Copy method - https://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-of-an-object-in-net-c-specifically
        public static Course DeepCopy<Course>(this Course course)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, course);
                stream.Position = 0;
                return (Course)formatter.Deserialize(stream);
            }

        }// DeepCopy()

    }// Utility

}

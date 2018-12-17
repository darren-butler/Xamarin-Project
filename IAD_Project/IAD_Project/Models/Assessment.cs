using System;

namespace IAD_Project.Models
{
    [Serializable()]
    public class Assessment
    {
        // 1. Vars
        public string Name { get; set; }

        public float Grade { get; set; }

        public float Weight { get; set; }


        // 2. Constructors
        public Assessment(string name, float weight)
        {
            Name = name;

            Grade = 0.0f;

            Weight = weight;

        }// Assessment(string float)

    }// Assessment

}

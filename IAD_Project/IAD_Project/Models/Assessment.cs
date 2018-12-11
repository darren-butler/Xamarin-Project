using System;
using System.Collections.Generic;
using System.Text;

namespace IAD_Project.Models
{
    public class Assessment
    {
        // Vars
        public string Name { get; set; }

        public float Grade { get; set; }

        public float Weight { get; set; }


        // Constructors
        public Assessment(string name, float weight)
        {
            Name = name;

            Grade = 0.0f;

            Weight = weight;

        }// Assessment(string float)

    }// Assessment

}

using System;
using System.Collections.Generic;
using System.Text;

namespace IAD_Project.Models
{
    public class Module
    {
        // Vars
        public string Name { get; set; }

        public float Credits { get; set; }

        public float Grade { get; set; }

        public List<Assessment> Assessments { get; set; }


        // Constructors
        public Module(string name, float credits)
        {
            Name = name;

            Credits = credits;

            Grade = 0.0f;

            Assessments = new List<Assessment>();

        }// Module(string, float)


        // Methods
        public void CalculateGrade()
        {
            Grade = 0;
            foreach (Assessment assessment in Assessments)
            {
                Grade += assessment.Grade * assessment.Weight;
            }
        }


    }// Module

}

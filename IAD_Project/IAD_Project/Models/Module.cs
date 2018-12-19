using System;
using System.Collections.Generic;

namespace IAD_Project.Models
{
    public class Module
    {
        // 1. Vars
        public string Name { get; set; }

        public float Credits { get; set; }

        public float Grade { get; set; }

        public List<Assessment> Assessments { get; set; }


        // 2. Constructors
        public Module(string name, float credits)
        {
            Name = name;

            Credits = credits;

            Grade = 0.0f;

            Assessments = new List<Assessment>();

        }// Module(string, float)


        // 3. Methods
        public void CalculateGrade()
        {
            Grade = 0;

            foreach (Assessment assessment in Assessments)
            {
                Grade += assessment.Grade * assessment.Weight;
            }

        }// CalculateGrade()

        public bool ValidateAssessmentWeights()
        {
            bool isInValid = true;
            float weightTotal = 0;

            foreach (Assessment assessment in Assessments)
            {
                weightTotal += assessment.Weight;
            }

            if (weightTotal > 1)
            {
                isInValid = false;
            }

            return isInValid;

        }// ValidateGradeWeights()

    }// Module

}

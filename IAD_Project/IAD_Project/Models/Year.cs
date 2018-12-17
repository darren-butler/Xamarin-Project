using System;
using System.Collections.Generic;

namespace IAD_Project.Models
{
    [Serializable()]
    public class Year
    {
        // 1. Vars
        public int YearNumber { get; set; }

        public float GradeAverage { get; set; }

        public List<Module> Modules { get; set; }


        // 2. Constructors
        public Year(int yearNum)
        {
            YearNumber = yearNum+1;

            GradeAverage = 0.0f;

            //YearCredits = 0.0f;

            Modules = new List<Module>();

        }// Year(int)


        // 3. Methods
        public void CalculateAverage()
        {
            float sum = 0;
            GradeAverage = 0;

            foreach (Module module in Modules)
            {
                module.CalculateGrade(); // need to call calculate method on each module

                sum += module.Grade;
            }

            GradeAverage = sum / Modules.Count;

        }// CalculateAverage()

    }// Year

}

using System;
using System.Collections.Generic;
using System.Text;

namespace IAD_Project.Models
{
    public class Year
    {
        // Vars
        public int YearNumber { get; set; }

        public float GradeAverage { get; set; }

        //public float YearCredits { get; set; }

        public List<Module> Modules { get; set; }


        // Constructors
        public Year(int yearNum)
        {
            YearNumber = yearNum+1;

            GradeAverage = 0.0f;

            //YearCredits = 0.0f;

            Modules = new List<Module>();

        }// Year(int)


        // Methods
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

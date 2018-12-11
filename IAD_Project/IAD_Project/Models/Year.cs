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

        public List<Module> Modules { get; set; }


        // Constructors
        public Year(int yearNum)
        {
            YearNumber = yearNum;

            GradeAverage = 0.0f;

            Modules = new List<Module>();
        }
    }// Year
}

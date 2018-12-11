using System;
using System.Collections.Generic;
using System.Text;

namespace IAD_Project.Models
{
    public class Course
    {
        // Vars
        public string Name { get; set; }

        public int NumOfYears { get; set; }

        public Year[] Years { get; set; }


        // Constructors
        public Course(string name, int numOfYears)
        {
            Name = name;

            NumOfYears = numOfYears;

            Years = new Year[NumOfYears];

            for (int i = 0; i < numOfYears; i++)
            {
                Years[i] = new Year(i);
            }
        }
    }// Course
}

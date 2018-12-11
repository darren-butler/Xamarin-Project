using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IAD_Project.Models;
using Newtonsoft.Json;
using System.IO;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CourseOverviewPage : ContentPage
	{
        Course course = new Course();

        public CourseOverviewPage ()
		{
			InitializeComponent ();
            course = Utility.DeserializeCourse();

            /*DEBUGGING
            lblCourseName.Text = course.Name;
            lblCurrentYear.Text = (course.Years[0].YearNumber + 1).ToString(); */
        }
    }
}
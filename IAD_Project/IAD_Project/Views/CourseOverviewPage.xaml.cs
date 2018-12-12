using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IAD_Project.Models;
using IAD_Project.Views;
using Newtonsoft.Json;
using System.IO;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CourseOverviewPage : ContentPage
	{
        Course course = new Course();
        int YearNum;

        public CourseOverviewPage ()
		{
			InitializeComponent ();
            course = Utility.DeserializeCourse();
            SetupYearButtons();

            /*DEBUGGING
            lblCourseName.Text = course.Name;
            lblCurrentYear.Text = (course.Years[0].YearNumber + 1).ToString(); */
        }

        private void SetupYearButtons()
        {

            for (int i = 0; i < course.NumOfYears; i++)
            {
                var btn = new Button();

                btn.Text = "Year " + (course.Years[i].YearNumber + 1).ToString() + " Average: " + (course.Years[i].GradeAverage).ToString();

                layout.Children.Add(btn);

            }// for
        }

        private async void btnYear_Clicked()
        {
            await Navigation.PushAsync(new YearPage());
        }

    }
}
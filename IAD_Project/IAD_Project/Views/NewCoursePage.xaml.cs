using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IAD_Project.Models;
using System.IO;
using Newtonsoft.Json;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewCoursePage : ContentPage
	{
		public NewCoursePage ()
		{
			InitializeComponent ();
		}

        private async void btnCreateCourse_Clicked(object sender, EventArgs e)
        {
            // if - both input boxes aren't empty
            if (entCourseName.Text != null && entCoureNumOfYears.Text != null)
            {
                // Create a new Course with input parameters & then serialize 
                Course course = new Course(entCourseName.Text, Int32.Parse(entCoureNumOfYears.Text));
                course.SerializeCourse();

                await Navigation.PushAsync(new CourseOverviewPage());
            }

        }// btnCreateCourse_Clicked()

    }

}
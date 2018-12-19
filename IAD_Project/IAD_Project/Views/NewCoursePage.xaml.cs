using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IAD_Project.Models;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewCoursePage : ContentPage
	{
		public NewCoursePage ()
		{
			InitializeComponent ();

        }// NewCoursePage()


        private async void btnCreateCourse_Clicked(object sender, EventArgs e)
        {
            // if - both input boxes aren't empty
            if (entCourseName.Text != null && entCoureNumOfYears.Text != null)
            {
                if(int.TryParse(entCoureNumOfYears.Text, out int numOfYears)) // test if input is int
                {
                    if (numOfYears >= 1 && numOfYears < 7) // test if input is within reasonable range
                    {
                        // Create a new Course with input parameters & then serialize 
                        Course course = new Course(entCourseName.Text, int.Parse(entCoureNumOfYears.Text));
                        course.SerializeCourse(); // save course to JSON file
                        await Navigation.PushAsync(new CourseOverviewPage(course), false);
                    }

                }

            }// if

        }// btnCreateCourse_Clicked()

    }// NewCoursePage

}
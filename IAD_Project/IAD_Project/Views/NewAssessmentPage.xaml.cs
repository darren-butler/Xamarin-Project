using IAD_Project.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewAssessmentPage : ContentPage
	{
        // Vars
        Course course = new Course();
        int YEAR_INDEX;
        int MODULE_INDEX;

        public NewAssessmentPage (Course c, int yearNum, int moduleIndex)
		{
			InitializeComponent ();

            // Initialize & Assign Course Variables
            course = c.DeepCopy();
            YEAR_INDEX = yearNum;
            MODULE_INDEX = moduleIndex;

        }// NewAssessmentPage()


        private async void btnAddNewAssessment_Clicked(object sender, EventArgs e)
        {
            // if - check if entry boxes are empty
            if(entAssessmentName.Text != null && entAssessmentWeight.Text != null)
            {
                // if - check if weight is a viable float
                if (float.TryParse(entAssessmentWeight.Text, out float weight))
                {
                    weight = weight / 100; // devide weight by 100 to conver from % to ratio

                    // if - check if wieght is within usable range
                    if (weight > 0 && weight <= 1)
                    {
                        // Create new Assessment with above paramteres and add to Assessments list
                        course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments.Add(new Assessment(entAssessmentName.Text, weight));

                        course.SerializeCourse(); // save course to JSON file
                        await Navigation.PushAsync(new ModuleOverviewPage(course, YEAR_INDEX, MODULE_INDEX), false);
                    }

                }

            }// if

        }// btnAddNewAssessment_Clicked()

    }// NewAssessmentPage

}
using IAD_Project.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssessmentOverviewPage : ContentPage
	{
        // Vars
        Course course = new Course();
        int YEAR_INDEX;
        int MODULE_INDEX;
        int ASSESSMENT_INDEX;
        
        bool isUpdateClicked = false;

        public AssessmentOverviewPage (Course c, int yearNum, int moduleIndex, int assessmentIndex)
		{
			InitializeComponent ();

            // Initialize & Assign Course Variables
            course = c.DeepCopy();
            YEAR_INDEX = yearNum;
            MODULE_INDEX = moduleIndex;
            ASSESSMENT_INDEX = assessmentIndex;

            displayAssessmentOverview();

        }// AssessmentOverviewPage()


        private void displayAssessmentOverview()
        {
            // Display Assessment Name & Weight
            lblAssessmentName.Text += course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Name;
            lblAssessmentWeight.Text += course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Weight*100 + "%";

            // if - Grade has not been added by user
            if (course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade == 0)
            {
                lblAssessmentGrade.Text += "n/a"; // set grade value to n/a
            }
            else
            {
                lblAssessmentGrade.Text += course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade.ToString("n2"); // set grade value to user entered value
            }

        }// displayAssessmentOverview()


        private void btnUpdateGrade_Clicked(object sender, EventArgs e)
        {
            // toggle isClicked bool
            isUpdateClicked = !isUpdateClicked; // https://stackoverflow.com/questions/12751194/nicer-code-for-toggling-a-bool-member

            // if - give the button alternating functionality, 1. show / hide grade entry box, 2. read in value from entry box and serialize course
            if(isUpdateClicked == true)
            {
                entUpdateGrade.IsVisible = true;
            }
            else if (isUpdateClicked == false)
            {
                course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade = float.Parse(entUpdateGrade.Text);
                lblAssessmentGrade.Text = "Grade: " + course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade.ToString();

                course.SerializeCourse(); // save course to JSON file

                entUpdateGrade.IsVisible = false;
            }

        }// btnUpdateGrade_Clicked()


        private async void btnBACK_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new ModuleOverviewPage(course, YEAR_INDEX, MODULE_INDEX));

        }// btnBACK_Clicked()

        private async void btnEditAssessment_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new EditAssessmentPage(course, YEAR_INDEX, MODULE_INDEX, ASSESSMENT_INDEX));
        }
    }// AssessmentOverviewPage

}
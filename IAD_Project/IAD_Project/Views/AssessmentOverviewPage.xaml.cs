using IAD_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssessmentOverviewPage : ContentPage
	{
        // Vars
        int YEARNUM;
        int MODULE_INDEX;
        int ASSESSMENT_INDEX;
        Course course = new Course();
        bool isUpdateClicked = false;

        public AssessmentOverviewPage (int yearNum, int moduleIndex, int assessmentIndex)
		{
			InitializeComponent ();

            // Initialize & Assign Course Variables
            course = Utility.DeserializeCourse();
            YEARNUM = yearNum;
            MODULE_INDEX = moduleIndex;
            ASSESSMENT_INDEX = assessmentIndex;

            displayAssessmentOverview();

        }// AssessmentOverviewPage()

        private void displayAssessmentOverview()
        {
            // Display Assessment Name & Weight
            lblAssessmentName.Text += course.Years[YEARNUM].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Name;
            lblAssessmentWeight.Text += course.Years[YEARNUM].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Weight;

            // if - Grade has not been added by user
            if (course.Years[YEARNUM].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade == 0)
            {
                lblAssessmentGrade.Text += "n/a"; // set grade value to n/a
            }
            else
            {
                lblAssessmentGrade.Text += course.Years[YEARNUM].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade; // set grade value to user entered value
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
                course.Years[YEARNUM].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade = float.Parse(entUpdateGrade.Text);
                lblAssessmentGrade.Text = "Grade: " + course.Years[YEARNUM].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade.ToString();
                course.SerializeCourse();

                entUpdateGrade.IsVisible = false;
            }

        }// btnUpdateGrade_Clicked()


        private async void btnBACK_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ModuleOverviewPage(YEARNUM, MODULE_INDEX));

        }// btnBACK_Clicked()
    }
}
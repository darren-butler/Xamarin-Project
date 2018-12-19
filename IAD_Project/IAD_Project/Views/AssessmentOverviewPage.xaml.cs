using IAD_Project.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssessmentOverviewPage : ContentPage
	{
        // Vars
        Course course;
        int YEAR_INDEX;
        int MODULE_INDEX;
        int ASSESSMENT_INDEX;
        
        bool isUpdateClicked = false;

        public AssessmentOverviewPage (int yearNum, int moduleIndex, int assessmentIndex)
		{
			InitializeComponent ();

            // Initialize & Assign Course Variables
            //course = c.DeepCopy();
            course = Utility.DeserializeCourse();
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
                lblAssessmentGrade.Text += "NA"; // set grade value to n/a
            }
            else
            {
                lblAssessmentGrade.Text += course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade.ToString("n2") + "%"; // set grade value to user entered value
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
                btnUpdateGrade.Text = "Update";
                entUpdateGrade.Placeholder = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade.ToString("n2");
            }
            else if (isUpdateClicked == false)
            {
                // if - check if entry boxes are empty
                if (entUpdateGrade.Text != null)
                {
                    // if - check if weight is a viable float
                    if (float.TryParse(entUpdateGrade.Text, out float grade))
                    {

                        // if - check if wieght is within usable range
                        if (grade >= 0 && grade <= 100)
                        {
                            course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade = grade;
                            course.SerializeCourse(); // save course to JSON file

                            lblAssessmentGrade.Text = "Grade: " + course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Grade.ToString();

                            entUpdateGrade.IsVisible = false;
                            btnUpdateGrade.Text = "Enter Grade";
                        }

                    }

                }

            }// if

        }// btnUpdateGrade_Clicked()


        private async void btnBACK_Clicked(object sender, EventArgs e)
        {

            //https://stackoverflow.com/questions/28477139/how-do-i-clear-the-navigation-stack
            var existingPages = Navigation.NavigationStack.ToList();

            // if navigation page stack is greater than ten, remove the bottom 7 pages // this was done to stop max heap size OOM error on android
            if (existingPages.Count > 10)
            {
                for (int i = 0; i < 7; i++)
                {
                    var page = existingPages[i];

                    Navigation.RemovePage(page);
                }
            }

            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new ModuleOverviewPage(YEAR_INDEX, MODULE_INDEX), false);

        }// btnBACK_Clicked()

        private async void btnEditAssessment_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new EditAssessmentPage(YEAR_INDEX, MODULE_INDEX, ASSESSMENT_INDEX), false);
        }

    }// AssessmentOverviewPage

}
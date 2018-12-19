using IAD_Project.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAssessmentPage : ContentPage
    {
        // Vars
        Course course;
        int YEAR_INDEX;
        int MODULE_INDEX;
        int ASSESSMENT_INDEX;

        bool isEditNameClicked = false;
        bool isEditWeightClicked = false;

        public EditAssessmentPage(int yearNum, int moduleIndex, int assessmentIndex)
        {
            InitializeComponent();

            // Initialize & Assign Course Variables
            course = Utility.DeserializeCourse();
            YEAR_INDEX = yearNum;
            MODULE_INDEX = moduleIndex;
            ASSESSMENT_INDEX = assessmentIndex;

            // UI setup
            lblTitle.Text = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Name;
            lblWeight.Text = "Weight: " + (course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Weight * 100).ToString("n2") + "%";

        }// EditAssessmentPage()


        private void btnEditName_Clicked(object sender, EventArgs e)
        {
            isEditNameClicked = !isEditNameClicked;

            if (isEditNameClicked)
            {
                entEditName.IsVisible = true;
                btnEditName.Text = "Update";
                entEditName.Placeholder = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Name;
            }
            else
            {
                course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Name = entEditName.Text;
                course.SerializeCourse(); // save course to JSON file

                lblTitle.Text = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Name; // update title label with new name

                entEditName.IsVisible = false;
                btnEditName.Text = "Edit Name";

            }// if

        }// btnEditName_Clicked()


        private void btnEditWeight_Clicked(object sender, EventArgs e)
        {
            isEditWeightClicked = !isEditWeightClicked;

            if (isEditWeightClicked)
            {
                entEditWeight.IsVisible = true;
                btnEditWeight.Text = "Update";
                entEditWeight.Placeholder = (course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Weight * 100).ToString("n2") + "%";
            }
            else
            {
                // if - check if entry boxes are empty
                if (entEditWeight.Text != null)
                {
                    // if - check if weight is a viable float
                    if (float.TryParse(entEditWeight.Text, out float weight))
                    {
                        weight = weight / 100; // devide weight by 100 to conver from % to ratio

                        // if - check if wieght is within usable range
                        if (weight > 0 && weight <= 1)
                        {
                            course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Weight = weight;
                            course.SerializeCourse(); // save course to JSON file

                            lblWeight.Text = "Weight: " + (course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Weight * 100).ToString("n2"); // update title weight

                            entEditWeight.IsVisible = false;
                            btnEditWeight.Text = "Edit Weight";
                        }

                    }

                }

            }// if

        }// btnEditWeight_Clicked()


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
            await Navigation.PushAsync(new AssessmentOverviewPage(YEAR_INDEX, MODULE_INDEX, ASSESSMENT_INDEX), false);

        }// btnBACK_Clicked()

        private async void btnDeleteAssessment_Clicked(object sender, EventArgs e)
        {
            course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments.RemoveAt(ASSESSMENT_INDEX);
            course.SerializeCourse(); // save course to JSON file

            await Navigation.PushAsync(new ModuleOverviewPage(YEAR_INDEX, MODULE_INDEX), false);

        }// btnDeleteAssessment_Clicked()
    }
}
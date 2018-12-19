using IAD_Project.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModuleOverviewPage : ContentPage
	{
        // Vars
        Course course;
        int YEAR_INDEX;
        int MODULE_INDEX;

        public ModuleOverviewPage(int yearNum, int moduleIndex)
        {
            InitializeComponent();

            // Initialize & Assign Course Variables
            course = Utility.DeserializeCourse();
            YEAR_INDEX = yearNum;
            MODULE_INDEX = moduleIndex;

            Display();

        }// ModuleOverviewPage()


        private void Display()
        {
            SetupPageTitles();
            SetupAssessmentButtons();

        }// Display()


        private void SetupPageTitles()
        {
            string grade = "NA";

            course.Years[YEAR_INDEX].Modules[MODULE_INDEX].CalculateGrade();
            course.SerializeCourse(); // save course to JSON file


            // if - test if user grade is NaN
            if (course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Grade != 0)
            {
                grade = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Grade.ToString("n2") + "%";
            }

            // Add Module name & credits to page labels
            lblModuleOverviewTitle.Text = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Name;
            lblModuleOverviewCredits.Text = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Credits + " Credits";
            lblModuleGrade.Text = "Grade: " + grade;

            // if - Validate if grade module assessment weight total exceed 1;
            if (!course.Years[YEAR_INDEX].Modules[MODULE_INDEX].ValidateAssessmentWeights())
            {
                btnWarningMessage.IsVisible = true; // display warning message
            }

        }// SetupPageTitles()


        private void SetupAssessmentButtons()
        {
            // Get length of Assessments list
            int numOfAssessments = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments.Count;

            // Create buttons for each Assessment Object
            for(int i = 0; i < numOfAssessments; i++)
            {
                // Same technique as in CourseOverviewPage
                Button btn = new Button();
                btn.Clicked += new EventHandler(btnAssessmentPage_Clicked);
                btn.Text = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[i].Name + ": " +
                    course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[i].Grade.ToString("n2") + "%";
                btn.ClassId = i.ToString();

                layout.Children.Add(btn);
            }

        }// SetupAssessmentButtons()


        private async void btnAssessmentPage_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int btnIndex = int.Parse(button.ClassId);

            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new AssessmentOverviewPage(YEAR_INDEX, MODULE_INDEX, btnIndex), false);

        }// btnAssessmentPage_Clicked()


        private async void btnAddAssessment_Clicked(object sender, EventArgs e)
        {
            int yearNum = YEAR_INDEX;
            int moduleInex = MODULE_INDEX;

            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new NewAssessmentPage(YEAR_INDEX, MODULE_INDEX), false);

        }// btnAddAssessment_Clicked()


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
            await Navigation.PushAsync(new YearOverviewPage(YEAR_INDEX), false);

        }// btnBACK_Clicked()

        private async void btnEditModule_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new EditModulePage(YEAR_INDEX, MODULE_INDEX), false);

        }// btnEditModule_Clicked()

    }// ModuleOverviewPage

}
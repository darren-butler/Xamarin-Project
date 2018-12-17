using IAD_Project.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModuleOverviewPage : ContentPage
	{
        // Vars
        Course course = new Course();
        int YEAR_INDEX;
        int MODULE_INDEX;

        public ModuleOverviewPage(Course c, int yearNum, int moduleIndex)
        {
            InitializeComponent();

            // Initialize & Assign Course Variables
            course = c.DeepCopy();
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
            lblModuleOverviewTitle.Text = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Name +
                " (" + course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Credits + " Credits)";
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
                btn.Text = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[i].Name + ", " +
                    course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[i].Grade + "%";
                btn.ClassId = i.ToString();

                layout.Children.Add(btn);
            }

        }// SetupAssessmentButtons()


        private async void btnAssessmentPage_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int btnIndex = int.Parse(button.ClassId);

            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new AssessmentOverviewPage(course, YEAR_INDEX, MODULE_INDEX, btnIndex));

        }// btnAssessmentPage_Clicked()


        private async void btnAddAssessment_Clicked(object sender, EventArgs e)
        {
            int yearNum = YEAR_INDEX;
            int moduleInex = MODULE_INDEX;

            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new NewAssessmentPage(course, YEAR_INDEX, MODULE_INDEX));

        }// btnAddAssessment_Clicked()


        private async void btnBACK_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new YearOverviewPage(course, YEAR_INDEX));

        }// btnBACK_Clicked()

    }// ModuleOverviewPage

}
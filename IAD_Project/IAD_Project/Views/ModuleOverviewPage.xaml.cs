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
	public partial class ModuleOverviewPage : ContentPage
	{
        // Vars
        int YEARNUM;
        int MODULE_INDEX;
        Course course = new Course();

        public ModuleOverviewPage(int yearNum, int moduleIndex)
        {
            InitializeComponent();

            // Initialize & Assign Course Variables
            YEARNUM = yearNum;
            MODULE_INDEX = moduleIndex;
            course = Utility.DeserializeCourse();

            course.Years[YEARNUM].Modules[MODULE_INDEX].CalculateGrade();

            Display();

        }// ModuleOverviewPage()

        private void Display()
        {
            SetupPageTitles();
            SetupAssessmentButtons();

        }// Display()

        private void SetupPageTitles()
        {
            // Add Module name & credits to page labels
            lblModuleOverviewTitle.Text = course.Years[YEARNUM].Modules[MODULE_INDEX].Name +
                " (" + course.Years[YEARNUM].Modules[MODULE_INDEX].Credits + " Credits)";
            lblModuleGrade.Text = "Grade: " + course.Years[YEARNUM].Modules[MODULE_INDEX].Grade.ToString("n2") + "%";

        }// SetupPageTitles()

        private void SetupAssessmentButtons()
        {
            // Get length of Assessments list
            int numOfAssessments = course.Years[YEARNUM].Modules[MODULE_INDEX].Assessments.Count;

            // Create buttons for each Assessment Object
            for(int i = 0; i < numOfAssessments; i++)
            {
                // Same technique as in CourseOverviewPage
                Button btn = new Button();
                btn.Clicked += new EventHandler(btnAssessmentPage_Clicked);
                btn.Text = course.Years[YEARNUM].Modules[MODULE_INDEX].Assessments[i].Name + ", " +
                    course.Years[YEARNUM].Modules[MODULE_INDEX].Assessments[i].Grade + "%";
                btn.ClassId = i.ToString();

                layout.Children.Add(btn);
            }

        }// SetupAssessmentButtons()

        private async void btnAssessmentPage_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int btnIndex = Int32.Parse(button.ClassId);
            await Navigation.PushAsync(new AssessmentOverviewPage(YEARNUM, MODULE_INDEX, btnIndex));

        }// btnAssessmentPage_Clicked()

        private async void btnAddAssessment_Clicked(object sender, EventArgs e)
        {
            int yearNum = YEARNUM;
            int moduleInex = MODULE_INDEX;

            await Navigation.PushAsync(new NewAssessmentPage(YEARNUM, MODULE_INDEX));

        }// btnAddAssessment_Clicked()

        private async void btnBACK_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new YearOverviewPage(YEARNUM));

        }// btnBACK_Clicked()

    }
}
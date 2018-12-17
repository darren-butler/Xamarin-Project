using IAD_Project.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class YearOverviewPage : ContentPage
	{
        // Vars
        Course course = new Course();
        int YEAR_INDEX;

        public YearOverviewPage (Course c, int yearNum)
		{           
            InitializeComponent ();

            // Initialize & Assign Course Variables
            course = c.DeepCopy();
            YEAR_INDEX = yearNum;

            Display();

        }// YearOverviewPage()


        private void Display()
        {
            SetupPageTitles();
            SetupModuleButtons();

        }// Display()


        private void SetupPageTitles()
        {
            string gradeAverage = "NA";

            course.Years[YEAR_INDEX].CalculateAverage();
            course.SerializeCourse(); // save course to JSON file

            if (course.Years[YEAR_INDEX].GradeAverage > 0 && course.Years[YEAR_INDEX].GradeAverage <= 100)
            {
                gradeAverage = course.Years[YEAR_INDEX].GradeAverage.ToString("n2") + "%";
            }

            // Add Course Name, Year Number & Year Average to title labels
            lblYearOverviewTitle.Text = course.Name + " Year: " + course.Years[YEAR_INDEX].YearNumber;
            lblYearAverage.Text = "GPA: " + gradeAverage;

        }// SetupPageTitles()


        private void SetupModuleButtons()
        {

            // Get length of Modules list
            int numOfModules = course.Years[YEAR_INDEX].Modules.Count;

            // Create & Add button for each Module in list
            for (int i = 0; i < numOfModules; i++)
            {
                Button btn = new Button();
                btn.Clicked += new EventHandler(btnModulePage_Clicked);
                btn.Text = course.Years[YEAR_INDEX].Modules[i].Name;
                btn.ClassId = i.ToString(); // Assign i to button class ID

                layout.Children.Add(btn);

            }// for

        }// SetupModuleButtons()


        private async void btnAddModule_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new NewModulePage(course, YEAR_INDEX));

        }// btnAddModule_Clicked()


        private async void btnModulePage_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button; // cast sender object to Button
            int btnIndex = int.Parse(button.ClassId); // parse out button ID (i)

            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new ModuleOverviewPage(course, YEAR_INDEX, btnIndex));

        }// btnModulePage_Clicked()


        private async void btnBACK_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new CourseOverviewPage(course));

        }// btnBACK_Clicked()

    }// YearOverviewPage

}
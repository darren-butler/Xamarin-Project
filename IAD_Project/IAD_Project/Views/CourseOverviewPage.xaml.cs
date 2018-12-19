using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IAD_Project.Models;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CourseOverviewPage : ContentPage
	{
        // Vars
        Course course;

        public CourseOverviewPage (Course c)
        {      	
			InitializeComponent();

            // Initialize & Assign Course Variables
            course = Utility.DeserializeCourse();

            // UI Setup
            SetupYearButtons();
            lblTitle.Text = course.Name;

        }// CourseOverviewPage()


        private void SetupYearButtons()
        {
            // Create buttons for each Year object
            for (int i = 0; i < course.NumOfYears; i++)
            {
                // https://stackoverflow.com/questions/6187944/how-can-i-create-a-dynamic-button-click-event-on-a-dynamic-button
                Button btn = new Button(); // Create new button
                btn.Clicked += new EventHandler(btnYearPage_Clicked); // Add YearOverviewPage Clicked Event to dynamic button

                if(course.Years[i].Modules.Count != 0)
                {
                    course.Years[i].CalculateAverage();
                    course.SerializeCourse(); // save course to JSON file
                }

                string gradeAverage = "NA";
                if (course.Years[i].GradeAverage > 0 && course.Years[i].GradeAverage <= 100)
                {
                    gradeAverage = course.Years[i].GradeAverage.ToString("n2") + "%";
                }

                btn.Text = "Year " + course.Years[i].YearNumber.ToString() + " Average: " + gradeAverage; // Add Year Details to button text
                btn.ClassId = i.ToString(); // Assign i to button class ID

                layout.Children.Add(btn); // Add button to stack layout

            }// for

        }// SetupYearButtons()


        protected async void btnYearPage_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int btnIndex = int.Parse(button.ClassId); // parse out button ID (i)

            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new YearOverviewPage(btnIndex), false);

        }// btnYearPage_Clicked()


        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new MainPage(), false);

        }// btnBack_Clicked()

    }// CourseOverviewPage

}
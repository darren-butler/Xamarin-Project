using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IAD_Project.Models;
using IAD_Project.Views;
using Newtonsoft.Json;
using System.IO;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CourseOverviewPage : ContentPage
	{
        public CourseOverviewPage (Course course)
        {      	
			InitializeComponent();
            SetupYearButtons(course);
            lblTitle.Text += course.Name + " Overview";

        }// CourseOverviewPage()

        private void SetupYearButtons(Course course)
        {
            // Create buttons for each Year object
            for (int i = 0; i < course.NumOfYears; i++)
            {
                // https://stackoverflow.com/questions/6187944/how-can-i-create-a-dynamic-button-click-event-on-a-dynamic-button
                Button btn = new Button(); // Create new button
                btn.Clicked += new EventHandler(btnYearPage_Clicked); // Add YearOverviewPage Clicked Event to dynamic button
                btn.Text = "Year " + course.Years[i].YearNumber.ToString() + ", Average: " + course.Years[i].GradeAverage.ToString(); // Add Year Details to button text
                btn.ClassId = i.ToString(); // Assign i to button class ID

                layout.Children.Add(btn); // Add button to stack layout

            }// for

        }// SetupYearButtons()

        protected async void btnYearPage_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int btnIndex = Int32.Parse(button.ClassId); // parse out button ID (i)

            await Navigation.PushAsync(new YearOverviewPage(btnIndex));

        }// btnYearPage_Clicked()

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();

        }// btnBack_Clicked()
    }
}
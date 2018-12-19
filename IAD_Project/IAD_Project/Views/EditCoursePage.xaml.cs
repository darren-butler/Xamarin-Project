using IAD_Project.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditCoursePage : ContentPage
	{
        // Vars
        Course course;
        bool isEditNameClicked = false;

        public EditCoursePage ()
		{
			InitializeComponent ();
            
            // Initialize & Assign Course Variables
            course = Utility.DeserializeCourse();

            // UI Setup
            lblTitle.Text = course.Name;

        }// EditCoursePage()


        private void btnEditName_Clicked(object sender, EventArgs e)
        {
            isEditNameClicked = !isEditNameClicked;

            if (isEditNameClicked)
            {
                entEditName.IsVisible = true;
                entEditName.Placeholder = course.Name;
                btnEditName.Text = "Update";
            }
            else
            {
                course.Name = entEditName.Text;
                course.SerializeCourse(); // save course to JSON file

                lblTitle.Text = course.Name; // update page title with new course name

                entEditName.IsVisible = false;
                btnEditName.Text = "Edit Name";

            }// if

        }// btnEditName_Clicked()


        private async void btnDeleteCourse_Clicked(object sender, EventArgs e)
        {
            Utility.DeleteCourse();
            await Navigation.PushAsync(new MainPage(), false);

        }// btnDeleteCourse_Clicked()

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
            await Navigation.PushAsync(new MainPage(), false);

        }// btnBACK_Clicked()
    }
}
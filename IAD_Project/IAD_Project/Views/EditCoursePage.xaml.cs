using IAD_Project.Models;
using System;
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
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new MainPage(), false);

        }// btnBACK_Clicked()
    }
}
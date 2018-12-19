using System;
using Xamarin.Forms;
using IAD_Project.Views;
using IAD_Project.Models;

namespace IAD_Project
{
    public partial class MainPage : ContentPage
    {
        // Vars
        Course course;

        public MainPage()
        {
            InitializeComponent();

            try // test if CourseData.txt file already exists
            {
                course = Utility.DeserializeCourse();
                btnCourseOverviewPage.IsVisible  = true;
                btnCourseOverviewPage.Text = course.Name;
                btnEditCourse.IsVisible = true;

            }
            catch
            {
                btnNewCourse.IsVisible = true;
            }

        }// MainPage()


        private async void btnNewCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewCoursePage(), false);

        }// btnNewCourse_Clicked()


        private async void btnCourseOverviewPage_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new CourseOverviewPage(course), false);

        }// btnCourseOverviewPage_Clicked()

        private async void btnEditCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCoursePage(), false);

        }// btnEditCourse_Clicked()


    }// MainPage

}

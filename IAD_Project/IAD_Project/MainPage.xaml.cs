using System;
using Xamarin.Forms;
using IAD_Project.Views;
using IAD_Project.Models;

namespace IAD_Project
{
    public partial class MainPage : ContentPage
    {
        // Vars
        Course course = new Course();

        public MainPage()
        {
            InitializeComponent();

            try // test if CourseData.txt file already exists
            {
                Course c = Utility.DeserializeCourse();
                course = c.DeepCopy();

                btnCourseOverviewPage.IsVisible = btnDeleteCourse.IsVisible = true;

            }
            catch
            {
                btnNewCourse.IsVisible = true;
            }

        }// MainPage()


        private async void btnNewCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewCoursePage());

        }// btnNewCourse_Clicked()


        private async void btnCourseOverviewPage_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new CourseOverviewPage(course));

        }// btnCourseOverviewPage_Clicked()

        private void btnDeleteCourse_Clicked(object sender, EventArgs e)
        {
            btnCourseOverviewPage.IsVisible = btnDeleteCourse.IsVisible = false;

            btnNewCourse.IsVisible = true;

        }
    }// MainPage

}

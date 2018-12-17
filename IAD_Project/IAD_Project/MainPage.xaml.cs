using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using IAD_Project.Views;
using IAD_Project.Models;
using System.IO;

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
                course = Utility.DeserializeCourse();
                btnCourseOverviewPage.IsVisible = true;
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
            await Navigation.PushAsync(new CourseOverviewPage(/*course*/));

        }// btnCourseOverviewPage_Clicked()
    }
}

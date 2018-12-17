using IAD_Project.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewModulePage : ContentPage
	{
        // Vars
        Course course = new Course();
        int YEAR_INDEX;
        
		public NewModulePage (Course c, int yearNum)
		{
			InitializeComponent ();

            // Initialize & Assign Course Variables
            course = c.DeepCopy();
            YEAR_INDEX = yearNum;

        }// NewModulePage()


        private async void btnAddNewModule_Clicked(object sender, EventArgs e)
        {
            // if - check if entry boxes are empty
            if(entModuleName.Text != null && entModuleCredits.Text != null)
            {
                // if - check if credits is a viable float
                if (float.TryParse(entModuleCredits.Text, out float credits))
                {
                    //if - check if credits is within reasonable range
                    if(credits > 0 && credits < 60)
                    {
                        // Construct new Module Object with parameters from entry boxes, then add it to Modules list
                        course.Years[YEAR_INDEX].Modules.Add(new Module(entModuleName.Text.ToString(), credits));
                        course.SerializeCourse(); // save course to JSON file

                        await Navigation.PushAsync(new YearOverviewPage(course, YEAR_INDEX));
                    }

                }

            }// if

        }// btnAddNewModule_Clicked()

    }// NewModulePage

}
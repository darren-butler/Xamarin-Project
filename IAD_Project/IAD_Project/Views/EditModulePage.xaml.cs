using IAD_Project.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditModulePage : ContentPage
	{
        // Vars
        Course course;
        int YEAR_INDEX;
        int MODULE_INDEX;

        bool isEditNameClicked = false;
        bool isEditCreditClicked = false;

        public EditModulePage (int yearNum, int moduleIndex)
		{
			InitializeComponent ();

            // Initialize & Assign Course Variables
            course = Utility.DeserializeCourse();
            YEAR_INDEX = yearNum;
            MODULE_INDEX = moduleIndex;

            // UI setup
            lblTitle.Text = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Name;
            lblTitleCredits.Text = "Credits: " + course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Credits.ToString();

        }// EditModulePage()

        private void btnEditName_Clicked(object sender, EventArgs e)
        {
            isEditNameClicked = !isEditNameClicked;

            if(isEditNameClicked)
            {
                entEditName.IsVisible = true;
                btnEditName.Text = "Update";
                entEditName.Placeholder = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Name;
            }
            else if (!isEditNameClicked && entEditName.Text != null)
            {
                course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Name = entEditName.Text;
                course.SerializeCourse(); // save course to JSON file    

                entEditName.IsVisible = false;
                btnEditName.Text = "Edit Name";

            }// if

        }// btnEditName_Clicked()


        private void btnEditCredits_Clicked(object sender, EventArgs e)
        {
            isEditCreditClicked = !isEditCreditClicked;

            if(isEditCreditClicked)
            {
                entEditCredits.IsVisible = true;
                btnEditCredits.Text = "Update";
                entEditCredits.Placeholder = course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Credits.ToString();
            }
            else
            {
                // if - check if entry boxes are empty
                if (entEditCredits.Text != null)
                {
                    // if - check if credits is a viable float
                    if (float.TryParse(entEditCredits.Text, out float credits))
                    {

                        // if - check if credits is within usable range
                        if (credits > 0 && credits <= 100)
                        {
                            course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Credits = credits;
                            course.SerializeCourse(); // save course to JSON file

                            lblTitleCredits.Text = "Credits: " + course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Credits.ToString();

                            entEditCredits.IsVisible = false;
                            btnEditCredits.Text = "Edit Credits";
                        }

                    }

                }

            }// if

        }// btnEditCredits_Clicked()


        private async void btnDeleteModile_Clicked(object sender, EventArgs e)
        {
            course.Years[YEAR_INDEX].Modules.RemoveAt(MODULE_INDEX);
            course.SerializeCourse(); // save course to JSON file

            await Navigation.PushAsync(new YearOverviewPage(YEAR_INDEX), false);

        }// btnDeleteModile_Clicked()

        private async void btnBACK_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new ModuleOverviewPage(YEAR_INDEX, MODULE_INDEX), false);

        }// btnBACK_Clicked()
    }
}
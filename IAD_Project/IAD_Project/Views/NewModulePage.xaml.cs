using IAD_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewModulePage : ContentPage
	{
        // Vars
        int YEARNUM;
        Course course = new Course();

		public NewModulePage (int yearNum)
		{
			InitializeComponent ();

            // Initialize & Assign Course Variables
            course = Utility.DeserializeCourse();
            YEARNUM = yearNum;

        }// NewModulePage()

        private async void btnAddNewModule_Clicked(object sender, EventArgs e)
        {
            if(entModuleName.Text != null && entModuleCredits.Text != null)
            {

                if (float.TryParse(entModuleCredits.Text, out float credits))
                {
                    if(credits > 0 && credits < 60)
                    {
                        // Construct new Module Object with parameters from entry boxes, then add it to Modules list
                        course.Years[YEARNUM].Modules.Add(new Module(entModuleName.Text.ToString(), credits));
                        course.SerializeCourse();

                        await Navigation.PushAsync(new YearOverviewPage(YEARNUM));
                    }

                }

            }


        }// btnAddNewModule_Clicked()
    }
}
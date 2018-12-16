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
	public partial class NewAssessmentPage : ContentPage
	{
        // Vars
        int YEARNUM;
        int MODULE_INDEX;
        Course course = new Course();

        public NewAssessmentPage (int yearNum, int moduleIndex)
		{
			InitializeComponent ();

            // Initialize & Assign Course Variables
            course = Utility.DeserializeCourse();
            YEARNUM = yearNum;
            MODULE_INDEX = moduleIndex;

        }// NewAssessmentPage()

        private async void btnAddNewAssessment_Clicked(object sender, EventArgs e)
        {
            // Read in Assessment Name & Weight from entry boxes
            string name = entAssessmentName.Text;
            float weight = float.Parse(entAssessmentWeight.Text);

            // Create new Assessment with above paramteres and add to Assessments list
            course.Years[YEARNUM].Modules[MODULE_INDEX].Assessments.Add(new Assessment(name, weight));
            course.SerializeCourse();

            await Navigation.PushAsync(new ModuleOverviewPage(YEARNUM, MODULE_INDEX));

        }// btnAddNewAssessment_Clicked()
    }
}
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
    public partial class EditAssessmentPage : ContentPage
    {
        // Vars
        Course course = new Course();
        int YEAR_INDEX;
        int MODULE_INDEX;
        int ASSESSMENT_INDEX;


        bool isEditNameClicked = false;
        bool isEditWeightClicked = false;

        public EditAssessmentPage(Course c, int yearNum, int moduleIndex, int assessmentIndex)
        {
            InitializeComponent();

            // Initialize & Assign Course Variables
            course = c.DeepCopy();
            YEAR_INDEX = yearNum;
            MODULE_INDEX = moduleIndex;
            ASSESSMENT_INDEX = assessmentIndex;

        }

        private void btnEditName_Clicked(object sender, EventArgs e)
        {
            isEditNameClicked = !isEditNameClicked;

            if (isEditNameClicked)
            {
                entEditName.IsVisible = true;
                btnEditName.Text = "Update";
            }
            else
            {
                course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Name = entEditName.Text;
                entEditName.IsVisible = false;
                btnEditName.Text = "Edit Name";
            }
        }

        private void btnEditWeight_Clicked(object sender, EventArgs e)
        {
            isEditWeightClicked = !isEditWeightClicked;

            if (isEditWeightClicked)
            {
                entEditWeight.IsVisible = true;
                btnEditWeight.Text = "Update";
            }
            else
            {
                // if - check if entry boxes are empty
                if (entEditWeight.Text != null)
                {
                    // if - check if weight is a viable float
                    if (float.TryParse(entEditWeight.Text, out float weight))
                    {
                        weight = weight / 100; // devide weight by 100 to conver from % to ratio

                        // if - check if wieght is within usable range
                        if (weight > 0 && weight <= 1)
                        {
                            course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments[ASSESSMENT_INDEX].Weight = weight;
                            entEditWeight.IsVisible = false;
                            btnEditWeight.Text = "Edit Weight";
                        }

                    }

                }// if

            }
        }

        private async void btnBACK_Clicked(object sender, EventArgs e)
        {
            course.SerializeCourse(); // save course to JSON file
            await Navigation.PushAsync(new AssessmentOverviewPage(course, YEAR_INDEX, MODULE_INDEX, ASSESSMENT_INDEX), false);

        }// btnBACK_Clicked()

        private async void btnDeleteAssessment_Clicked(object sender, EventArgs e)
        {
            course.Years[YEAR_INDEX].Modules[MODULE_INDEX].Assessments.RemoveAt(ASSESSMENT_INDEX);
            course.SerializeCourse(); // save course to JSON file

            await Navigation.PushAsync(new ModuleOverviewPage(course, YEAR_INDEX, MODULE_INDEX), false);
        }
    }
}
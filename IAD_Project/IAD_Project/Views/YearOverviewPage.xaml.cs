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
	public partial class YearOverviewPage : ContentPage
	{
        // Vars
        int YEARNUM;
        Course course = new Course();

        public YearOverviewPage (int yearNum)
		{           
            InitializeComponent ();

            // Initialize & Assign Course Variables
            course = Utility.DeserializeCourse();
            YEARNUM = yearNum;

            SetupModuleButtons(course, yearNum);

            // Add Course Name, Year Number & Year Average to title labels
            lblYearOverviewTitle.Text = course.Name + " Year: " + course.Years[yearNum].YearNumber;
            lblYearAverage.Text = "Average: " + course.Years[yearNum].GradeAverage;

        }// YearOverviewPage()

        private void SetupModuleButtons(Course course, int yearNum)
        {
            //if (course.Years[yearNum].Modules.Count == 0)
            //{
            //    Label lbl = new Label();
            //    lbl.Text = "DEBUG - YOU HAVE NO MODULES";
            //    layout.Children.Add(lbl);
            //}
            //else
            //{

            // Get length of Moduls list
            int numOfModules = course.Years[YEARNUM].Modules.Count;

            // Create & Add button for each Module in list
            for (int i = 0; i < numOfModules; i++)
            {
                Button btn = new Button();
                btn.Clicked += new EventHandler(btnModulePage_Clicked);
                btn.Text = course.Years[YEARNUM].Modules[i].Name;
                btn.ClassId = i.ToString(); // Assign i to button class ID

                layout.Children.Add(btn);
            }
            //}

        }// SetupModuleButtons()

        private async void btnAddModule_Clicked(object sender, EventArgs e)
        {
            int yearNum = YEARNUM;
            await Navigation.PushAsync(new NewModulePage(yearNum));

        }// btnAddModule_Clicked()

        private async void btnModulePage_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button; // cast sender object to Button
            int btnIndex = Int32.Parse(button.ClassId); // parse out button ID (i)

            await Navigation.PushAsync(new ModuleOverviewPage(YEARNUM, btnIndex));

        }// btnModulePage_Clicked()

        private async void btnBACK_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseOverviewPage(course));

        }// btnBACK_Clicked()

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using IAD_Project.Views;


namespace IAD_Project
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnNewCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewCoursePage());
        }
    }
}

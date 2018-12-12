using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IAD_Project.Models;

namespace IAD_Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class YearPage : ContentPage
	{
		public YearPage ()
		{
			InitializeComponent ();

            //lblDEBUG.Text = course.Name;
            
		}

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
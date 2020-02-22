using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusinessAnalyst.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountCreatePage : ContentPage
	{
		public AccountCreatePage ()
		{
			InitializeComponent ();
		}

        private void OnCreate(object sender, EventArgs e)
        {
            if(isPinValid())
            {
                Application.Current.Properties["PIN"] = enterPin.Text;

                Navigation.PushAsync(new HomePage());
            }
        }

        private bool isPinValid()
        {
            if(string.IsNullOrWhiteSpace(enterPin.Text))
            {
                DisplayAlert("Error", "Enter Pin Required", "Close");
                return false;
            }
            else if(string.IsNullOrWhiteSpace(confirmPin.Text))
            {
                DisplayAlert("Error", "Confirm Pin Required", "Close");
                return false;
            }
            else if(confirmPin.Text == enterPin.Text)
            {
                return true;
            }
            else
            {
                DisplayAlert("Error", "Pin doesnot match", "Close");
                return false;
            }
        }
    }
}
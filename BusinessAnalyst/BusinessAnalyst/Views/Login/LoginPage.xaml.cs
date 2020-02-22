using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusinessAnalyst.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Finger Print can be aded here
        /// https://docs.microsoft.com/en-us/xamarin/android/platform/fingerprint-authentication/
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLogin(object sender, EventArgs e)
        {
            if (isPinCorrect())
            {
                Navigation.PushAsync(new HomePage());
            }
        }

        private bool isPinCorrect()
        {
            if (string.IsNullOrWhiteSpace(enterPin.Text))
            {
                DisplayAlert("Error", "Pin required", "Close");
                return false;
            }
            else if (enterPin.Text == Application.Current.Properties["PIN"].ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
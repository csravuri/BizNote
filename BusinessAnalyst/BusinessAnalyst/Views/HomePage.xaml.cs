using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAnalyst.Views.Items;
using BusinessAnalyst.Views.Parties;
using BusinessAnalyst.Views.Report;
using BusinessAnalyst.Views.Transactions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusinessAnalyst.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void OnCreateParty(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PartyCreatePage("Create Party"));
        }

        private async void OnCreateItem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemCreatePage("Create Item"));
        }

        private async void OnTransaction(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectTransactionPage());
        }

        private async void OnReports(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReportsPage());
        }

        private async void OnCompanyInfo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompanyInfoPage());
        }

        private async void OnAbout(object sender, EventArgs e)
        {
            await DisplayAlert("😊", "Made with ❤", "Close");
        }
    }
}
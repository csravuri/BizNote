using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAnalyst.Views.Parties;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BusinessAnalyst.Models.Enumarations;

namespace BusinessAnalyst.Views.Transactions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectTransactionPage : ContentPage
    {
        public SelectTransactionPage()
        {
            InitializeComponent();
        }

        private async void OnPurchase(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPartyPage("Select Supplier", false, PartyType.Supplier));
        }

        private async void OnSale(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPartyPage("Select Customer", false, PartyType.Customer));
        }

        private async void OnMisc(object sender, EventArgs e)
        {
            await DisplayAlert("Info", "Not decided what to include here 😂", "Close");
        }
    }
}
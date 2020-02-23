using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAnalyst.Models;
using BusinessAnalyst.Views.Items;
using BusinessAnalyst.Views.Parties;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BusinessAnalyst.Models.Enumarations;

namespace BusinessAnalyst.Views.Report
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsPage : ContentPage
    {
        public ReportsPage()
        {
            InitializeComponent();
        }



        private async void OnItemsSearch(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchItemPage("Search Item"));
        }

        private async void OnPartySearch(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPartyPage("Search Party"));
        }

        private async void OnTransactionsSearch(object sender, EventArgs e)
        {
            DbTransaction dbTransaction = DbTransaction.GetInstance();
            List<Transaction> transactions = await dbTransaction.GetTransactions();
            await DisplayAlert("Good", $"Transactions count: {transactions.Count}", "OK");
        }

        private async void OnPurchaseRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchRegisterPage("Purchase Register", TransactionType.Purchase));
        }

        private async void OnSaleRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchRegisterPage("Sale Register", TransactionType.Sale));
        }
    }
}
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

namespace BusinessAnalyst.Views.Transactions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        private Party _party;

        public TransactionsPage()
        {
            InitializeComponent();

        }

        public TransactionsPage(Party party) : this()
        {
            Title = GetTransactionName(party.PartyType);

            _party = party;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            partyName.Text = $"{_party.PartyType}: {_party.PartyName}";
            balance.Text = "0";

        }

        private async void OnAdd(object sender, EventArgs e)
        {
            await DisplayAlert("Error", "This feature is not available in trail version", "Close");
        }

        private void SelectItem(object sender, EventArgs e)
        {

            var itemNameLabel = sender as Label;
            var grid = itemNameLabel.Parent as Grid;
            var mrpEntry = grid.Children.Where(c => Grid.GetRow(c) == Grid.GetRow(itemNameLabel) && Grid.GetColumn(c) == 2).FirstOrDefault() as Entry;

            var searchItemPage = new SearchItemPage("Select Item", false);
            searchItemPage.ItemsListView.ItemTapped += (source, args) =>
            {
                var item = args.Item as Item;
                itemNameLabel.Text = item.ItemName;
                mrpEntry.Text = item.MRP;
                Navigation.PopAsync();
            };

            Navigation.PushAsync(searchItemPage);
        }

        private void OnQtyEnter(object sender, TextChangedEventArgs e)
        {
            var qtyEntry = sender as Entry;
            var grid = qtyEntry.Parent as Grid;
            var mrpEntry = grid.Children.Where(c => Grid.GetRow(c) == Grid.GetRow(qtyEntry) && Grid.GetColumn(c) == 2).FirstOrDefault() as Entry;
            var amountEntry = grid.Children.Where(c => Grid.GetRow(c) == Grid.GetRow(qtyEntry) && Grid.GetColumn(c) == 3).FirstOrDefault() as Entry;

            amountEntry.Text = (Utils.ToDecimal(qtyEntry.Text) * Utils.ToDecimal(mrpEntry.Text)).ToString();
        }

        private string GetTransactionName(PartyType? partyType)
        {
            switch (partyType)
            {
                case PartyType.Supplier:
                    return "Purchase";
                case PartyType.Customer:
                    return "Sale";
                default:
                    return "Transaction";
            }
        }

        private async void OnSave(object sender, EventArgs e)
        {
            string billNo = await Utils.GetBillNo(_party.PartyType);
            List<Transaction> transactions = new List<Transaction>();
            int gridRowCount = itemList.Children.Select(c => Grid.GetRow(c)).Max();


            for (int i = 1; i <= gridRowCount; i++)
            {
                var itemNameLabel = itemList.Children.Where(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == 0).FirstOrDefault() as Label;
                if (string.IsNullOrWhiteSpace(itemNameLabel.Text))
                {
                    continue;
                }

                var qtyEntry = itemList.Children.Where(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == 1).FirstOrDefault() as Entry;
                if (string.IsNullOrWhiteSpace(qtyEntry.Text))
                {
                    await DisplayAlert("Error", "Qty is required", "OK");
                    return;
                }

                var mrpEntry = itemList.Children.Where(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == 2).FirstOrDefault() as Entry;
                if (string.IsNullOrWhiteSpace(mrpEntry.Text))
                {
                    mrpEntry.Text = "0";
                }

                var amountEntry = itemList.Children.Where(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == 3).FirstOrDefault() as Entry;

                if (string.IsNullOrWhiteSpace(amountEntry.Text))
                {
                    amountEntry.Text = "0";
                }

                transactions.Add(new Transaction
                {
                    BillNo = billNo,
                    TransactionType = Utils.GetTransactionType(_party.PartyType),
                    PartyName = _party.PartyName,
                    PartyType = _party.PartyType,
                    City = _party.City,
                    ItemName = itemNameLabel.Text,
                    Qty = qtyEntry.Text,
                    MRP = mrpEntry.Text,
                    Amount = amountEntry.Text,
                    CreatedDate = DateTime.Now
                });
            }

            DbTransaction dbTransaction = DbTransaction.GetInstance();

            dbTransaction.InsertAllAsync(transactions);

            Register register = new Register
            {
                BillNo = billNo,
                TransactionDate = DateTime.Now,
                PartyName = _party.PartyName,
                City = _party.City,
                TransactionType = Utils.GetTransactionType(_party.PartyType),
                Amount = transactions.Sum(t => Utils.ToDecimal(t.Amount)).ToString()
            };

            dbTransaction.InsertAsync(register);

            await DisplayAlert("Success", $"Bill No: {billNo} generated", "OK");

            OnClear(null, null);

        }

        private void OnClear(object sender, EventArgs e)
        {
            InitializeComponent();
            OnAppearing();
        }
    }
}
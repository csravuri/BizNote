using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAnalyst.Models;
using BusinessAnalyst.Views.Transactions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BusinessAnalyst.Models.Enumarations;

namespace BusinessAnalyst.Views.Parties
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPartyPage : ContentPage
    {
        private ObservableCollection<Party> _partiesFromDB;
        private DbTransaction dbTransaction;
        private bool _isFromSearch;
        private PartyType? _partyType;

        public SearchPartyPage()
        {
            InitializeComponent();

        }

        public SearchPartyPage(string title, bool isSearch = true, PartyType? partyType = null) : this()
        {
            Title = title;
            _isFromSearch = isSearch;
            _partyType = partyType;
            dbTransaction = DbTransaction.GetInstance();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            IEnumerable<Party> parties = await dbTransaction.GetParties();
            if (_partyType != null)
            {
                parties = parties.Where(x => x.PartyType == _partyType);
            }

            _partiesFromDB = new ObservableCollection<Party>(parties);

            this.listView.ItemsSource = _partiesFromDB;
        }

        private void OnDelete(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            Party selectedItem = menuItem.CommandParameter as Party;

            _partiesFromDB.Remove(selectedItem);
            dbTransaction.DeleteAsync(selectedItem);
        }

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue) && _partiesFromDB != null)
            {
                this.listView.ItemsSource = _partiesFromDB.Where(x => x.PartyName.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            else
            {
                this.listView.ItemsSource = _partiesFromDB;
            }
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.listView.SelectedItem = null;
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Party selctedParty = e.Item as Party;
            if (_isFromSearch)
            {
                await Navigation.PushAsync(new PartyCreatePage("Update Party", selctedParty));
            }
            else
            {
                await Navigation.PushAsync(new TransactionsPage(selctedParty));
                
                
            }
        }
    }
}
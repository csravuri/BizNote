using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAnalyst.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusinessAnalyst.Views.Items
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchItemPage : ContentPage
    {
        private DbTransaction dbTransaction;
        private ObservableCollection<Item> _itemsFromDB;
        private bool _isFromSearch;

        public ListView ItemsListView { get { return listView; } }

        public SearchItemPage()
        {
            InitializeComponent();
        }

        public SearchItemPage(string title, bool isSearch = true) : this()
        {
            Title = title;
            _isFromSearch = isSearch;
            dbTransaction = DbTransaction.GetInstance();            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var items = await dbTransaction.GetItems();
            _itemsFromDB = new ObservableCollection<Item>(items);

            listView.ItemsSource = _itemsFromDB;
        }

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue) && _itemsFromDB != null)
            {
                this.listView.ItemsSource = _itemsFromDB.Where(x => x.ItemName.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            else
            {
                this.listView.ItemsSource = _itemsFromDB;
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            Item selectedItem = menuItem.CommandParameter as Item;

            _itemsFromDB.Remove(selectedItem);
            dbTransaction.DeleteAsync(selectedItem);

        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (_isFromSearch)
            {
                Item item = e.Item as Item;
                await Navigation.PushAsync(new ItemCreatePage("Update Item", item));
            }
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.listView.SelectedItem = null;
        }
    }
}
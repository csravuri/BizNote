using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAnalyst.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusinessAnalyst.Views.Items
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemCreatePage : ContentPage
    {
        private Item _item;
        private DbTransaction dbTransaction;

        public ItemCreatePage()
        {
            InitializeComponent();
        }

        public ItemCreatePage(string title, Item item = null) : this()
        {            
            this.Title = title;

            this._item = item ?? new Item();

            BindingContext = _item;

            dbTransaction = DbTransaction.GetInstance();

        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); 
        }

        private async void OnSave(object sender, EventArgs e)
        {
            if (isItemValid())
            {
                //List<Item> a = await dbTransaction.GetItems();

                CleanData();
                if (_item.ItemID != 0)
                {
                    dbTransaction.UpdateAsync(_item);
                    await DisplayAlert("Success", $"{_item.ItemName} updated", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    dbTransaction.InsertAsync(_item);
                    await DisplayAlert("Success", $"{_item.ItemName} saved", "OK");
                }

                ClearControls();

            }

        }
        private void CleanData()
        {
            if(string.IsNullOrWhiteSpace(_item.MRP))
            {
                _item.MRP = "0";
            }
        }

        private bool isItemValid()
        {
            decimal temp;
            if (string.IsNullOrWhiteSpace(_item.ItemName))
            {
                DisplayAlert("Error", "Item Name is Required", "Close");
                return false;
            }
            else if(!string.IsNullOrWhiteSpace(_item.MRP) && !decimal.TryParse(_item.MRP, out temp))
            {
                DisplayAlert("Error", "MRP should be Numaric", "Close");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ClearControls()
        {
            _item = new Item();
            BindingContext = _item;
        }
    }
}
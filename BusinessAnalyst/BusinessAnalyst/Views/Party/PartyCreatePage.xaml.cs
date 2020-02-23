using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAnalyst.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BusinessAnalyst.Models.Enumarations;

namespace BusinessAnalyst.Views.Parties
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartyCreatePage : ContentPage
    {
        private Party _party;
        private Array partTypeList;
        private DbTransaction dbTransaction;
        public PartyCreatePage(string title, Party party = null)
        {
            InitializeComponent();

            partTypeList = Enum.GetValues(typeof(PartyType));

            LoadValuesInPicker();

            Title = title;
            _party = party ?? new Party();

            BindingContext = _party;

            dbTransaction = DbTransaction.GetInstance();

        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnSave(object sender, EventArgs e)
        {
            List<Party> p = await dbTransaction.GetParties();
            if(isPartyValid())
            {
                if (_party.PartyID != 0)
                {
                    dbTransaction.UpdateAsync(_party);
                    await DisplayAlert("Success", $"{_party.PartyName} updated", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    dbTransaction.InsertAsync(_party);
                    await DisplayAlert("Success", $"{_party.PartyName} saved", "OK");
                }

                ClearControls();
            }

        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private bool isPartyValid()
        {
            if(string.IsNullOrWhiteSpace(_party.PartyName))
            {
                DisplayAlert("Error", "Party Name is Required", "Close");
                return false;
            }
            else
            {
                return true;
            }      


        }


        private void PartyType_Changed(object sender, EventArgs e)
        {
            PartyType? selectedPartyType = (PartyType?)(sender as Picker).SelectedItem;

            switch(selectedPartyType)
            {
                case PartyType.Customer:
                    _party.isCreditor = false;
                    break;
                case PartyType.Supplier:
                case PartyType.Loan:
                    _party.isCreditor = true;
                    break;
                case PartyType.Other:
                case PartyType.OwnBankAccount:
                default:
                    _party.isCreditor = null;
                    break;                    
            }
        }

        private void LoadValuesInPicker()
        {
            try
            {
                partyType.ItemsSource = partTypeList;

            }
            catch (Exception e)
            {
                DisplayAlert("Exception", e.Message, "Close");
            }
        }

        private void ClearControls()
        {
            _party = new Party();
            BindingContext = _party;
        }


    }
}
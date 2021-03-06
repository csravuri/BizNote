﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAnalyst.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BusinessAnalyst.Models.Enumarations;

namespace BusinessAnalyst.Views.Report
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchRegisterPage : ContentPage
    {
        public SearchRegisterPage()
        {
            InitializeComponent();
        }

        public SearchRegisterPage(string title, TransactionType? transactionType) : this()
        {            
            Title = title;
        }

        private void OnSelectParty(object sender, EventArgs e)
        {
            // select party from list
            DisplayAlert("select", "Soon", "OK");
        }

        private async void OnSearch(object sender, EventArgs e)
        {
            DbTransaction dbTransaction = DbTransaction.GetInstance();
            List<Register> allRegisters = await dbTransaction.GetRegisters();

            List<Register> filteredRegisters = GetFilteredRegisters(allRegisters);

            registerDataGrid.RowDefinitions = new RowDefinitionCollection();
            registerDataGrid.Children.Clear();
            for (int i=0; i < filteredRegisters.Count; i++)
            {
                registerDataGrid.RowDefinitions.Add(new RowDefinition());
                registerDataGrid.Children.Add(new Label { BackgroundColor = Color.White, Text = filteredRegisters[i].TransactionDate.ToString("dd/MM/yyyy") }, 0, i);
                registerDataGrid.Children.Add(new Label { BackgroundColor = Color.White, Text = filteredRegisters[i].BillNo }, 1, i);
                registerDataGrid.Children.Add(new Label { BackgroundColor = Color.White, Text = filteredRegisters[i].PartyName }, 2, i);
                registerDataGrid.Children.Add(new Label { BackgroundColor = Color.White, Text = filteredRegisters[i].City }, 3, i);
                registerDataGrid.Children.Add(new Label { BackgroundColor = Color.White, Text = filteredRegisters[i].Amount }, 4, i);
            }

        }

        private List<Register> GetFilteredRegisters(List<Register> registers)
        {
            if(!partyLabel.Text.Contains("..."))
            {
                registers = registers.Where(x => x.PartyName == partyLabel.Text).ToList();
            }

            if (fromDatePicker.Date != null)
            {
                registers = registers.Where(x => x.TransactionDate >= fromDatePicker.Date).ToList();
            }
            
            if (toDatePicker.Date != null)
            {
                registers = registers.Where(x => x.TransactionDate.Date <= toDatePicker.Date).ToList();
            }

            if (!string.IsNullOrWhiteSpace(billNo.Text))
            {
                registers = registers.Where(x => x.BillNo == billNo.Text).ToList();
            }

            if (!string.IsNullOrWhiteSpace(city.Text))
            {
                registers = registers.Where(x => x.City == city.Text).ToList();
            }

            return registers;
        }

    }
}
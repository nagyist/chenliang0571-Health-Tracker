using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using Health_Tracker.Model;

namespace Health_Tracker
{
    public partial class NewItemPage : PhoneApplicationPage
    {
        public NewItemPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;
        }


        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            CategoryBean tb = (CategoryBean)selectedCategory.SelectedItem;
            if (String.IsNullOrEmpty(tb.CategoryName))
            {
                MessageBox.Show("Error: Category is not selected.");
                return;
            }
            Items newItem = new Items
            {
                CategoryID = tb.ID,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                IsActivity = true,
                UpdateTime = DateTime.Now

            };
            App.ViewModel.AddItem(newItem, tb);

            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
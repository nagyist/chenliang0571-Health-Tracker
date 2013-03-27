using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Health_Tracker.Model;

namespace Health_Tracker
{
    public partial class NewCategoryPage : PhoneApplicationPage
    {
        public NewCategoryPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;
        }

        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.newCategoryName.Text))
            {
                MessageBox.Show("Error: Category Name is not empty.");
                return;
            }
            Categories newCategoary = new Categories
            {
                Name = this.newCategoryName.Text,
                IsActivity = true,
                UpdateTime = DateTime.Now
            };
            App.ViewModel.AddCategory(newCategoary);

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ComponentModel;

namespace Health_Tracker
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
        }

        private const int PAGE_INDEX = 0;
        private const int PAGE_REVIEW = 1;
        private const int PAGE_CATEGOARY = 2;

        private void newTaskAppBarButton_Click(object sender, EventArgs e)
        {
            if (this.mainPivot.SelectedIndex == PAGE_INDEX)
            {
                NavigationService.Navigate(new Uri("/NewItemPage.xaml", UriKind.Relative));
            }
            else if (this.mainPivot.SelectedIndex == PAGE_CATEGOARY)
            {
                NavigationService.Navigate(new Uri("/NewCategoryPage.xaml", UriKind.Relative));
            }
        }

        private void mainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case PAGE_INDEX:
                case PAGE_CATEGOARY:
                    ApplicationBar.IsVisible = true;
                    break;
                case PAGE_REVIEW:
                    ApplicationBar.IsVisible = false;
                    break;
            }
        }

        private void itemMenu1_Click(object sender, RoutedEventArgs e)
        {
            //item
            MenuItem mi = (MenuItem)sender;
            App.ViewModel.DeleteItem(Int32.Parse(mi.Tag + ""));
        }

        private void itemMenu2_Click(object sender, RoutedEventArgs e)
        {
            //item
            MenuItem mi = (MenuItem)sender;
            App.ViewModel.DeleteItem(Int32.Parse(mi.Tag + ""));
        }

        private void itemMenu3_Click(object sender, RoutedEventArgs e)
        {
            //category
            MenuItem mi = (MenuItem)sender;
            App.ViewModel.DeleteCategory(Int32.Parse(mi.Tag + ""));
        }

        //private void TextBlock_KeyUp_1(object sender, KeyEventArgs e)
        //{
        //    NavigationService.Navigate(new Uri("/EditCategoryPage.xaml", UriKind.Relative));
        //}
    }
}
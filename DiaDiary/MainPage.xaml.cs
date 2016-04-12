using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;

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

namespace TypeOneControl
{
    
    public partial class MainPage : PhoneApplicationPage {

        // Constructor
        public MainPage() {

            InitializeComponent();

        }


        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            if (MainListBox.SelectedIndex == 0) {
                NavigationService.Navigate(new Uri("/GlucosePage.xaml", UriKind.Relative));
            } else if (MainListBox.SelectedIndex == 1) {
                NavigationService.Navigate(new Uri("/ShotsPage.xaml", UriKind.Relative));
            } else if (MainListBox.SelectedIndex == 2) {
                NavigationService.Navigate(new Uri("/FoodPage.xaml", UriKind.Relative));
            } else if (MainListBox.SelectedIndex == 3) {
                NavigationService.Navigate(new Uri("/ActivityPage.xaml", UriKind.Relative));
            } else if (MainListBox.SelectedIndex == 4) {
                NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
            } else if (MainListBox.SelectedIndex == 5) {
                NavigationService.Navigate(new Uri("/ReportPage.xaml", UriKind.Relative));
            }

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;

        }

    }

}
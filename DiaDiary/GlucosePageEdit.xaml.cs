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
using System.Windows.Data;
using Microsoft.Phone.Controls;
  

namespace PhoneAppDB {
    
    public partial class GlucosePageEdit : PhoneApplicationPage {

        public GlucoseRecord glucoseRecord;
        
        private AppDataContext dataContext;

        public GlucosePageEdit()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            string selectedIndex = "";
            
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex)) {
                int index = int.Parse(selectedIndex);
                this.DataContext = this;
                dataContext = new AppDataContext(AppDataContext.DBConnectionString);
                var item = dataContext.GlucoseRecordsTable.Single(x => x.GlucoseRecordId == index);
                glucoseRecord = item;
                LayoutRoot.DataContext = glucoseRecord;
            }



        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e) {

        }

        private void Save_Record(object sender, RoutedEventArgs e) {

            glucoseRecord.GlucoseRecordValue = float.Parse(RecordValue.Text);
            dataContext.SubmitChanges();
            NavigationService.Navigate(new Uri("/GlucosePage.xaml", UriKind.Relative));

            //var tag = (sender as Button).Tag;
            //int t = Convert.ToInt16(tag);
            //NavigationService.Navigate(new Uri("/GlucosePageEdit.xaml?selectedItem=" + t, UriKind.Relative));

        }

        private void Delete_Record(object sender, RoutedEventArgs e) {

            dataContext.GlucoseRecordsTable.DeleteOnSubmit(glucoseRecord);
            dataContext.SubmitChanges();

            var tag = (sender as Button).Tag;
            int t = Convert.ToInt16(tag);
            NavigationService.Navigate(new Uri("/GlucosePage.xaml", UriKind.Relative));

        }

        private void Cancel(object sender, RoutedEventArgs e) {

            NavigationService.Navigate(new Uri("/GlucosePage.xaml", UriKind.Relative));

        }
    
    }

}
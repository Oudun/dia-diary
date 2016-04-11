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

        private bool isFormValid = true;

        public GlucosePageEdit()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {

            string selectedIndex = "";
            dataContext = new AppDataContext(AppDataContext.DBConnectionString);

            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex)) {
                int index = int.Parse(selectedIndex);
                this.DataContext = this;
                glucoseRecord = dataContext.GlucoseRecordsTable.Single(x => x.GlucoseRecordId == index);
            } else {
                glucoseRecord = new GlucoseRecord {
                    GlucoseRecordValue = -1,
                    GlucoseTime = DateTime.Now
                };                         
            }
            
            LayoutRoot.DataContext = glucoseRecord;

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e) {

        }

        private void Save_Record(object sender, RoutedEventArgs e) {

            Validate_Value(sender, e);

            if (!isFormValid) {
                return;
            }


            //Updating glucose level
            glucoseRecord.GlucoseRecordValue = float.Parse(RecordValue.Text);

            //Updating time glucose level taken
            DateTime dateTime = glucoseRecord.GlucoseTime;
            DateTime updatedDate = DateTime.Parse(RecordDate.ValueString);
            DateTime updatedTime = DateTime.Parse(RecordTime.ValueString);
            DateTime newDateTime = new DateTime(
                updatedDate.Year, updatedDate.Month, updatedDate.Day,
                updatedTime.Hour, updatedTime.Minute, 0
                );
            glucoseRecord.GlucoseTime = newDateTime;

            if (glucoseRecord.GlucoseRecordId == 0) {
                dataContext.GlucoseRecordsTable.InsertOnSubmit(glucoseRecord);
            } 

            dataContext.SubmitChanges();
            
            //Navigating back to records list
            NavigationService.Navigate(new Uri("/GlucosePage.xaml", UriKind.Relative));            
                
        }

        private void Delete_Record(object sender, RoutedEventArgs e) {

            if (MessageBox.Show("Are you sure?", "Delete Item", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {

                if (glucoseRecord.GlucoseRecordId > 0) {

                    dataContext.GlucoseRecordsTable.DeleteOnSubmit(glucoseRecord);
                    dataContext.SubmitChanges();

                    var tag = (sender as Button).Tag;
                    int t = Convert.ToInt16(tag);
                    NavigationService.Navigate(new Uri("/GlucosePage.xaml", UriKind.Relative));

                }

            }
            
        }

        private void Cancel(object sender, RoutedEventArgs e) {

            NavigationService.Navigate(new Uri("/GlucosePage.xaml", UriKind.Relative));

        }

        private void Validate_Value(object sender, RoutedEventArgs e) { 

            try {
                float value = float.Parse(RecordValue.Text);
                if (value <= 0 || value > 100) {
                    isFormValid = false;
                    RecordValue.BorderBrush = new SolidColorBrush(Colors.Red);
                } else {
                    isFormValid = true;
                    RecordValue.BorderBrush = new SolidColorBrush(Colors.Transparent);               
                }
            } catch {
                isFormValid = false;
                RecordValue.BorderBrush = new SolidColorBrush(Colors.Red); 
            }
        
        }
    
    }

}
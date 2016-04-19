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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TypeOneControl {
    
    public partial class GlucosePage : PhoneApplicationPage, INotifyPropertyChanged  {

        public event PropertyChangedEventHandler PropertyChanged;
        
        private AppDataContext dataContext;

        private ObservableCollection<GlucoseRecord> _glucoseRecords;

        public ObservableCollection<GlucoseRecord> GlucoseRecords {
            
            get {
                return _glucoseRecords;
            }
            
            set {
                if (_glucoseRecords != value) {
                    _glucoseRecords = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("GlucoseRecords"));
                }
            }

        }


        public GlucosePage() {

            InitializeComponent();
            this.DataContext = this;
            dataContext = new AppDataContext(AppDataContext.DBConnectionString);
        
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {

            var glucoseRecordsInDB = from GlucoseRecord glucoseRecord in dataContext.GlucoseRecordsTable select glucoseRecord;
            GlucoseRecords = new ObservableCollection<GlucoseRecord>(glucoseRecordsInDB);
            base.OnNavigatedTo(e);

        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e) {

        }

        private void New_Record(object sender, RoutedEventArgs e) {

            NavigationService.Navigate(new Uri("/GlucosePageEdit.xaml", UriKind.Relative));

        }


        private void Edit_Record(object sender, RoutedEventArgs e) {

            var tag = (sender as Button).Tag;
            int t = Convert.ToInt16(tag);
            NavigationService.Navigate(new Uri("/GlucosePageEdit.xaml?selectedItem=" + t, UriKind.Relative));

        }

        private void Button_Click(object sender, RoutedEventArgs e) {

        }

    }

}
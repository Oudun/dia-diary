using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TypeOneControl {

    public partial class ShotPage : PhoneApplicationPage, INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        private AppDataContext dataContext;

        private ObservableCollection<ShotRecord> _shotRecords;

        private ObservableCollection<Insulin> _insulins;

        public ObservableCollection<Insulin> Insulins {
            
            get {
                return _insulins;
            }

            set {
                if (_insulins != value) {
                    _insulins = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Insulins"));
                }
            }
        }

        public ObservableCollection<ShotRecord> ShotRecords {

            get {
                return _shotRecords;
            }

            set {
                if (_shotRecords != value) {
                    _shotRecords = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ShotRecords"));
                }
            }

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {

            shotRecordsList.ItemsSource = from shotRecord in dataContext.ShotRecordsTable
                                          join insulin in dataContext.InsulinsTable on shotRecord.InsulinId equals insulin.Id
                                          select new ShotRecordExtended (shotRecord.Id,shotRecord.Value,insulin.Name,shotRecord.DateTime);

            //var insulinInDB = from Insulin insulin in dataContext.InsulinsTable where (insulin.Field<bool>("InUse") == true) select insulin;

            var insulinInDB = from Insulin insulin in dataContext.InsulinsTable where (insulin.InUse == true) select insulin;

            Insulins = new ObservableCollection<Insulin>(insulinInDB);
            base.OnNavigatedTo(e);

        }
        
        public ShotPage()  {
            InitializeComponent();
            this.DataContext = this;
            dataContext = new AppDataContext(AppDataContext.DBConnectionString);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e) {

        }
        
        private void New_Record(object sender, RoutedEventArgs e) {
            var tag = (sender as Button).Tag;
            int t = Convert.ToInt16(tag);
            NavigationService.Navigate(new Uri("/ShotPageEdit.xaml?insulinId="+t, UriKind.Relative));
        }

        private void Edit_Record(object sender, RoutedEventArgs e) {
            var tag = (sender as Button).Tag;
            int t = Convert.ToInt16(tag);
            NavigationService.Navigate(new Uri("/GlucosePageEdit.xaml?selectedItem=" + t, UriKind.Relative));
        }

    }

    public class ShotRecordExtended {

        public ShotRecordExtended() { 
        
        }

        public ShotRecordExtended(int aId, float aValue, String aName, DateTime aDateTime) {
            this.Id = aId;
            this.Value = aValue;
            this.Name = aName;
            this.DateTime = aDateTime;
        }

        private int _id;

        private float _value;

        private String _name;

        private DateTime _dateTime;

        public int Id {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }

        public float Value {
            get {
                return _value;
            }
            set {
                _value = value;
            }
        }

        public String Name {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public DateTime DateTime {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = value;
            }
        }

    
    }

}
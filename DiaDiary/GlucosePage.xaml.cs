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

namespace PhoneAppDB
{
    public partial class GlucosePage : PhoneApplicationPage {

        private AppDataContext dataContext;

        //public DateTime GlucoseTime { get; set; }

        private ObservableCollection<GlucoseRecord> _glucoseRecords;
        public ObservableCollection<GlucoseRecord> GlucoseRecords
        {
            get
            {

                _glucoseRecords = new ObservableCollection<GlucoseRecord>();
                _glucoseRecords.Add(new GlucoseRecord { GlucoseRecordId = 1, GlucoseRecordValue = 1333, GlucoseTime = new DateTime() });
                _glucoseRecords.Add(new GlucoseRecord { GlucoseRecordId = 1, GlucoseRecordValue = 1222, GlucoseTime = new DateTime() });
                return _glucoseRecords;
            }
            set
            {
                if (_glucoseRecords != value)
                {
                    _glucoseRecords = value;

                }
            }
        }


        public GlucosePage()
        {

            InitializeComponent();
            this.DataContext = this;
            dataContext = new AppDataContext(AppDataContext.DBConnectionString);
            //GlucoseTime = new DateTime();

            //appdatacontext appdatacontext = new appdatacontext(appdatacontext.dbconnectionstring);
            //glucoserecord glucoserecordone = new glucoserecord();
            //glucoserecordone.glucoserecordvalue = 1;
            //glucoserecordone.glucoserecordvalue = 1;
            //appdatacontext.glucoserecordstable.insertonsubmit(glucoserecordone);

            //this.datacontext = appdatacontext;

            //ToDoItem newToDo = new ToDoItem { ItemName = newToDoTextBox.Text };
            //// Add a to-do item to the observable collection.
            //ToDoItems.Add(newToDo);
            //// Add a to-do item to the local database.
            //toDoDB.ToDoItems.InsertOnSubmit(newToDo);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {

            // SAMPLE: Define the query to gather all of the to-do items.
            // var toDoItemsInDB = from ToDoItem todo in toDoDB.ToDoItems select todo;

            //var glucoseRecordsInDB = from GlucoseRecord glucoseRecord in dataContext.GlucoseRecordsTable select glucoseRecord;
            //GlucoseRecords = new ObservableCollection<GlucoseRecord>(glucoseRecordsInDB);

            _glucoseRecords = new ObservableCollection<GlucoseRecord>();
            _glucoseRecords.Add(new GlucoseRecord { GlucoseRecordId = 1, GlucoseRecordValue = 1, GlucoseTime = new DateTime() });
            _glucoseRecords.Add(new GlucoseRecord { GlucoseRecordId = 1, GlucoseRecordValue = 1, GlucoseTime = new DateTime() });

            base.OnNavigatedTo(e);

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
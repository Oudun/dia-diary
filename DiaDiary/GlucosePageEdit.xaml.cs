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

        public GlucoseRecord GlucoseRecord;
        
        private AppDataContext dataContext;

        public GlucosePageEdit()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            string selectedIndex = "";

            GlucoseRecord gk = new GlucoseRecord();
            
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);
                this.DataContext = this;
                dataContext = new AppDataContext(AppDataContext.DBConnectionString);

//                var glucoseRecordsInDB = from GlucoseRecord glucoseRecord in dataContext.GlucoseRecordsTable select glucoseRecord;
//                GlucoseRecords = new ObservableCollection<GlucoseRecord>(glucoseRecordsInDB);
//                base.OnNavigatedTo(e);

                var item = dataContext.GlucoseRecordsTable.Single(x => x.GlucoseRecordId == index);

                gk = item;

//                gk.GlucoseRecordId = index;
                LayoutRoot.DataContext = gk;
            }



        }
    
    }

}
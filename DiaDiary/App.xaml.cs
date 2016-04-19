using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TypeOneControl
{

    public partial class App : Application {

        public PhoneApplicationFrame RootFrame { get; private set; }
        
        public App() {
            
            UnhandledException += Application_UnhandledException;
            InitializeComponent();
            InitializePhoneApplication();

            if (System.Diagnostics.Debugger.IsAttached) {
                
                Application.Current.Host.Settings.EnableFrameRateCounter = true;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            }

            //File file  = "Data Source=appdata:/AppData.sdf; File Mode = read only;"
            using (AppDataContext db = new AppDataContext(AppDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == false) {
                    
                    db.CreateDatabase();

                    AppDataContext dataContext = new AppDataContext(AppDataContext.DBConnectionString);

                    ObservableCollection<GlucoseRecord> _glucoseRecords = new ObservableCollection<GlucoseRecord>();

                    _glucoseRecords.Add(new GlucoseRecord(13.2f, DateTime.Now.Add(new TimeSpan(1, 12, 32))));
                    _glucoseRecords.Add(new GlucoseRecord(11.1f, DateTime.Now.Add(new TimeSpan(3, 2, 32))));
                    _glucoseRecords.Add(new GlucoseRecord(7.2f, DateTime.Now.Add(new TimeSpan(3, 33, 32))));
                    _glucoseRecords.Add(new GlucoseRecord(6.5f, DateTime.Now.Add(new TimeSpan(4, 13, 32))));
                    _glucoseRecords.Add(new GlucoseRecord(10.4f, DateTime.Now.Add(new TimeSpan(6, 34, 32))));
                    _glucoseRecords.Add(new GlucoseRecord(12.2f, DateTime.Now.Add(new TimeSpan(10, 2, 32))));
                    _glucoseRecords.Add(new GlucoseRecord(22.0f, DateTime.Now.Add(new TimeSpan(10, 12, 32))));
                    _glucoseRecords.Add(new GlucoseRecord(3.2f, DateTime.Now.Add(new TimeSpan(12, 14, 32))));
                    _glucoseRecords.Add(new GlucoseRecord(7.4f, DateTime.Now.Add(new TimeSpan(12, 23, 32))));
                    _glucoseRecords.Add(new GlucoseRecord(12.2f, DateTime.Now.Add(new TimeSpan(20, 21, 32))));
                    
                    dataContext.GlucoseRecordsTable.InsertAllOnSubmit(_glucoseRecords);

                    ObservableCollection<ShotRecord> _shotRecords = new ObservableCollection<ShotRecord>();

                    _shotRecords.Add(new ShotRecord(13.2f, 1, DateTime.Now.Add(new TimeSpan(1, 12, 32))));
                    _shotRecords.Add(new ShotRecord(11.1f, 1, DateTime.Now.Add(new TimeSpan(3, 2, 32))));
                    _shotRecords.Add(new ShotRecord(7.2f, 1, DateTime.Now.Add(new TimeSpan(3, 33, 32))));
                    _shotRecords.Add(new ShotRecord(6.5f, 3, DateTime.Now.Add(new TimeSpan(4, 13, 32))));
                    _shotRecords.Add(new ShotRecord(10.4f, 1, DateTime.Now.Add(new TimeSpan(6, 34, 32))));
                    _shotRecords.Add(new ShotRecord(12.2f, 1, DateTime.Now.Add(new TimeSpan(10, 2, 32))));
                    _shotRecords.Add(new ShotRecord(22.0f, 1, DateTime.Now.Add(new TimeSpan(10, 12, 32))));
                    _shotRecords.Add(new ShotRecord(3.2f, 3, DateTime.Now.Add(new TimeSpan(12, 14, 32))));
                    _shotRecords.Add(new ShotRecord(7.4f, 1, DateTime.Now.Add(new TimeSpan(12, 23, 32))));
                    _shotRecords.Add(new ShotRecord(12.2f, 1, DateTime.Now.Add(new TimeSpan(20, 21, 32))));

                    dataContext.ShotRecordsTable.InsertAllOnSubmit(_shotRecords);
                    
                    ObservableCollection<Insulin> _insulins = new ObservableCollection<Insulin>();
                    _insulins.Add(new Insulin("Humulin", "ins_humulin", true));
                    _insulins.Add(new Insulin("Humalog", "ins_humalog", false));
                    _insulins.Add(new Insulin("Apidra", "ins_apidra", true));

                    dataContext.InsulinsTable.InsertAllOnSubmit(_insulins);
                    
                    ObservableCollection<MealUnit> _mealUnits = new ObservableCollection<MealUnit>();
                     
                    _mealUnits.Add(new MealUnit(1, "piece", "meal_unit_piece"));
                    _mealUnits.Add(new MealUnit(2, "glass", "meal_unit_glass"));
                    _mealUnits.Add(new MealUnit(3, "gramm", "meal_unit_gramm"));
                    
                    dataContext.MealUnitsTable.InsertAllOnSubmit(_mealUnits);
                    
                                     
                    ObservableCollection<Meal> _meals = new ObservableCollection<Meal>();
                    _meals.Add(new Meal("Bread", "meal_bread", 1, 1));
                    _meals.Add(new Meal("Milk", "meal_milk", 2, 1));
                    _meals.Add(new Meal("Porridge", "meal_porridge", 3, 1));

                    dataContext.MealsTable.InsertAllOnSubmit(_meals);
                    
                    dataContext.SubmitChanges();

                } else {
                    System.Diagnostics.Debug.WriteLine("Database 2 present");
                }
            }
        
        }

        private void Application_Launching(object sender, LaunchingEventArgs e) { }

        private void Application_Activated(object sender, ActivatedEventArgs e) { }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e) { }

        private void Application_Closing(object sender, ClosingEventArgs e) { }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e) {
            
            if (System.Diagnostics.Debugger.IsAttached) {
                System.Diagnostics.Debugger.Break();
            }
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) {
            
            if (System.Diagnostics.Debugger.IsAttached) {
                System.Diagnostics.Debugger.Break();
            }

        }
        #region Phone application initialization

        private bool phoneApplicationInitialized = false;

        
        private void InitializePhoneApplication() {
            
            if (phoneApplicationInitialized)
                return;

            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;
            phoneApplicationInitialized = true;

        }

        
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e) {

            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }

}
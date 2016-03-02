﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneAppDB {

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

            
            using (ToDoDataContext db = new ToDoDataContext(ToDoDataContext.DBConnectionString)) {
                if (db.DatabaseExists() == false) {
                    db.CreateDatabase();
                } else {
                    System.Diagnostics.Debug.WriteLine("Database present");
                }
            }

            using (AppDataContext db = new AppDataContext(AppDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == false) {
                    db.CreateDatabase();
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
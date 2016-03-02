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

namespace PhoneAppDB
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {


        // Data context for the local database
        private ToDoDataContext toDoDB;

        // Define an observable collection property that controls can bind to.
        private ObservableCollection<ToDoItem> _toDoItems;
        public ObservableCollection<ToDoItem> ToDoItems
        {
            get
            {
                return _toDoItems;
            }
            set
            {
                if (_toDoItems != value)
                {
                    _toDoItems = value;
                    NotifyPropertyChanged("ToDoItems");
                }
            }
        }

        

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
            // Connect to the database and instantiate data context.
            toDoDB = new ToDoDataContext(ToDoDataContext.DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;
        }


        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
              return;

            // Navigate to the new page
            if (MainListBox.SelectedIndex == 0) {
                NavigationService.Navigate(new Uri("/GlucosePage.xaml", UriKind.Relative));
            }
            else if (MainListBox.SelectedIndex == 1)
            {
                NavigationService.Navigate(new Uri("/ShotsPage.xaml", UriKind.Relative));
            }
            else if (MainListBox.SelectedIndex == 2)
            {
                NavigationService.Navigate(new Uri("/FoodPage.xaml", UriKind.Relative));
            }
            else if (MainListBox.SelectedIndex == 3)
            {
                NavigationService.Navigate(new Uri("/ActivityPage.xaml", UriKind.Relative));
            }
            else if (MainListBox.SelectedIndex == 4)
            {
                NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
            }
            else if (MainListBox.SelectedIndex == 5)
            {
                NavigationService.Navigate(new Uri("/ReportPage.xaml", UriKind.Relative));
            }
                

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }

        private void deleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast parameter as a button.
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                ToDoItem toDoForDelete = button.DataContext as ToDoItem;

                // Remove the to-do item from the observable collection.
                ToDoItems.Remove(toDoForDelete);

                // Remove the to-do item from the local database.
                toDoDB.ToDoItems.DeleteOnSubmit(toDoForDelete);

                // Save changes to the database.
                toDoDB.SubmitChanges();

                // Put the focus back to the main page.
                this.Focus();
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            toDoDB.SubmitChanges();
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Define the query to gather all of the to-do items.
            var toDoItemsInDB = from ToDoItem todo in toDoDB.ToDoItems
                                select todo;

            // Execute the query and place the results into a collection.
            ToDoItems = new ObservableCollection<ToDoItem>(toDoItemsInDB);

            // Call the base method.
            base.OnNavigatedTo(e);
        }




        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }


    public class ToDoDataContext : DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        //public static string DBConnectionString = "Data Source=isostore:/ToDo.sdf";
        public static string DBConnectionString = "Data Source=isostore:/ToDo.sdf";

        // Pass the connection string to the base class.
        public ToDoDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a single table for the to-do items.
        public Table<ToDoItem> ToDoItems;
    }

    [Table]
    public class ToDoItem : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _toDoItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ToDoItemId
        {
            get
            {
                return _toDoItemId;
            }
            set
            {
                if (_toDoItemId != value)
                {
                    NotifyPropertyChanging("ToDoItemId");
                    _toDoItemId = value;
                    NotifyPropertyChanged("ToDoItemId");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _itemName;

        [Column]
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                if (_itemName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        // Define completion value: private field, public property and database column.
        private bool _isComplete;

        [Column]
        public bool IsComplete
        {
            get
            {
                return _isComplete;
            }
            set
            {
                if (_isComplete != value)
                {
                    NotifyPropertyChanging("IsComplete");
                    _isComplete = value;
                    NotifyPropertyChanged("IsComplete");
                }
            }
        }
        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

}
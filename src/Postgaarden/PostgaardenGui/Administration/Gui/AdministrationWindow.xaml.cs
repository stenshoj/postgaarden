using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Postgaarden.Connection.Sqlite;
using Postgaarden.Crud.Equipments;
using Postgaarden.Crud.Persons;
using Postgaarden.Crud.Rooms;
using Postgaarden.Crud.Users;
using Postgaarden.Model.Persons;
using Postgaarden.Model.Rooms;
using Postgaarden.Model.Users;
using System.Diagnostics;

namespace PostgaardenGui.Administration.Gui
{
    /*
        Developed by Martin Hansen
    */

    /// <summary>
    /// Interaction logic for AdministrationWindow.xaml
    /// </summary>
    public partial class AdministrationWindow : Window
    {
        public AdministrationWindow()
        {
            InitializeComponent();

            var connection = SqliteDatabaseConnection.GetInstance(Properties.Settings.Default.Postgaarden);
            var equipmentCrud = new SqliteEquipmentCrud(connection);
            roomCrud = new SqliteRoomCrud(connection, equipmentCrud);
            roomHandler = new RoomHandler(roomCrud);
            userCrud = new SqliteUserCrud(connection);
            employeeCrud = new SqliteEmployeeCrud(connection);
        }

        private readonly RoomHandler roomHandler;
        private readonly SqliteRoomCrud roomCrud;
        private readonly SqliteEmployeeCrud employeeCrud;
        private readonly SqliteUserCrud userCrud;

        public ObservableCollection<string> EquipmentFiltlerObservableCollection { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Room> RoomObservableCollection { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<User> UserObservableCollection { get; set; }
        public ObservableCollection<Employee> EmployeeObservableCollection { get; set; }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Vil du fortsætte til hovedvinduet idet du lukker?", "Advarsel",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    new MainWindow().Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    this.Close();
                    break;
            }
        }

        #region Rooms Meethods

        private void AddFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            EquipmentFiltlerObservableCollection.Add(FilterTextTextBox.Text);
            FilterTextTextBox.Text = "";
        }

        private void RemoveFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            EquipmentFiltlerObservableCollection.Remove((string)EquipmentFilterListBox.SelectedItem);
        }

        private void FilterTextTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            AddFilterButton.IsEnabled = FilterTextTextBox.Text != "";
        }

        private void EquipmentFilterListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveFilterButton.IsEnabled = true;
        }

        private void SearchEquipmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var size = SetSizeTextBox.Text.Equals("") ? "0" : SetSizeTextBox.Text;
                var sizeRooms = new List<Room>(roomHandler.Filter(Convert.ToInt32(size), SetMinimumCheckBox.IsChecked ?? true));
                var equipmentRooms = new List<Room>(roomHandler.Filter(EquipmentFiltlerObservableCollection));
                RoomObservableCollection = new ObservableCollection<Room>(sizeRooms.Intersect(equipmentRooms));
                RoomListBox.ItemsSource = RoomObservableCollection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                System.Windows.MessageBox.Show("Venligst kontrollér de indtastede søgekriterier", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateRoomButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var createRoomWindow = new CreateRoomWindow();

                if (createRoomWindow.ShowDialog() != true) return;
                roomCrud.Create(createRoomWindow.ConferenceRoom);
                RoomObservableCollection.Add(createRoomWindow.ConferenceRoom);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                System.Windows.MessageBox.Show("Der opstod en fejl i forsøget på at oprette et rum", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditRoomButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var createRoomWindow = new CreateRoomWindow((ConferenceRoom)RoomListBox.SelectedItem);

                if (createRoomWindow.ShowDialog() != true) return;
                roomCrud.Update(createRoomWindow.ConferenceRoom);

                var view = CollectionViewSource.GetDefaultView(RoomObservableCollection);
                view.Refresh();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                System.Windows.MessageBox.Show("Der opstod en fejl i forsøget på at redigere et rum", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteRoomButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = RoomListBox.SelectedItems.Cast<object>().ToList();

                var warningMessage = items.Count > 1 ? "Er du sikker på at du vil slette de markerede lokaler?" : "Er du sikker på at du vil slette dette lokale?";

                var result = MessageBox.Show(warningMessage, "Advarsel", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result != MessageBoxResult.Yes) return;
                foreach (var item in items)
                {
                    roomCrud.Delete((ConferenceRoom)item);
                    RoomObservableCollection.Remove((ConferenceRoom)item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                System.Windows.MessageBox.Show("Der opstod en fejl i forsøget på at slette et eller flere rum", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RoomListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteRoomButton.IsEnabled = RoomListBox.SelectedItem != null;
            var selected = RoomListBox.SelectedItems.Count;
            DeleteRoomButton.Content = selected > 1 ? $"Slet ({selected})" : "Slet";
            EditRoomButton.IsEnabled = selected == 1;
        }

        #endregion

        #region Employees Methods

        private void EmployeesTabItem_OnLoaded(object sender, RoutedEventArgs e)
        {
            EmployeeObservableCollection = new ObservableCollection<Employee>(employeeCrud.Read());
        }

        private void CreateEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var createEmployeeWindow = new CreateEmployeeWindow();

                if (createEmployeeWindow.ShowDialog() != true) return;
                employeeCrud.Create(createEmployeeWindow.Employee);
                EmployeeObservableCollection.Add(createEmployeeWindow.Employee);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                System.Windows.MessageBox.Show("Der opstod en fejl i forsøget på at oprette en medarbejder", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var createEmployeeWindow = new CreateEmployeeWindow((Employee)EmployeeListView.SelectedItem);

                if (createEmployeeWindow.ShowDialog() != true) return;
                employeeCrud.Update(createEmployeeWindow.Employee);

                var view = CollectionViewSource.GetDefaultView(EmployeeObservableCollection);
                view.Refresh();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                System.Windows.MessageBox.Show("Der opstod en fejl i forsøget på at redigere en medarbejder", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = EmployeeListView.SelectedItems.Cast<object>().ToList();

                var warningMessage = items.Count > 1 ? "Er du sikker på at du vil slette de markerede medarbejdere?" : "Er du sikker på at du vil slette denne medarbejder?";

                var result = MessageBox.Show(warningMessage, "Advarsel", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result != MessageBoxResult.Yes) return;
                foreach (var item in items)
                {
                    employeeCrud.Delete((Employee)item);
                    EmployeeObservableCollection.Remove((Employee)item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                System.Windows.MessageBox.Show("Der opstod en fejl i forsøget på at slette en eller flere medarbejdere", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EmployeeListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteEmployeeButton.IsEnabled = EmployeeListView.SelectedItem != null;
            var selected = EmployeeListView.SelectedItems.Count;
            DeleteEmployeeButton.Content = selected > 1 ? $"Slet ({selected})" : "Slet";
            EditEmployeeButton.IsEnabled = selected == 1;
        }

        #endregion

        #region Users Methods

        private void SetAdministratorCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            ApplyButton.IsEnabled = true;
        }

        private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Implement this
            /*
            var dataBaseUsers = new ObservableCollection<User>(userCrud.Read());
            var updateUsers = dataBaseUsers.Intersect(UserObservableCollection);
            foreach (var user in updateUsers)
            {
                userCrud.Update(user);
            }
            */

            ApplyButton.IsEnabled = false;
        }

        private void UsersTabItem_OnLoaded(object sender, RoutedEventArgs e)
        {
            UserObservableCollection = new ObservableCollection<User>(userCrud.Read());
            DataContext = this;
        }

        #endregion
    }
}

using Postgaarden;
using Postgaarden.Connection.Sqlite;
using Postgaarden.Crud.Equipments;
using Postgaarden.Crud.Persons;
using Postgaarden.Crud.Rooms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PostgaardenGui
{
    /// Made by Christoffer
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public ObservableCollection<Booking> Bookings {get; set;} = new ObservableCollection<Booking>();
        private BookingCrud bookingCrud;
        public MainWindow()
        {           
            InitializeComponent();

            var empCrud = new SqliteEmployeeCrud(SqliteDatabaseConnection.GetInstance());
            var cusCrud = new SqliteCustomerCrud(SqliteDatabaseConnection.GetInstance());
            var equiCrud = new SqliteEquipmentCrud(SqliteDatabaseConnection.GetInstance());
            var roomCrud = new SqliteRoomCrud(SqliteDatabaseConnection.GetInstance(), equiCrud);
            bookingCrud = new SqliteBookingCrud(SqliteDatabaseConnection.GetInstance(), roomCrud, cusCrud, empCrud);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Bookings = new ObservableCollection<Booking>(bookingCrud.Read());            
            BookingList.ItemsSource = Bookings;
        }

        private void CreateBookingButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEdit create = new CreateEdit("Create", Bookings);
            create.ShowDialog();
        }

        private void EditBookingButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEdit edit = new CreateEdit("Edit", Bookings, (Booking)BookingList.SelectedItems[0]);
            Booking selecteditem = (Booking)BookingList.SelectedItems[0];
            edit.StartTimePicker.Text = ((Booking)selecteditem).StartTime.ToString();
            edit.EndTimeTextBox.Text = ((Booking)selecteditem).EndTime.ToString();
            edit.EmployeeIdTextBox.Text = ((Booking)selecteditem).Employee.Name;
            edit.PriceTextBox.Text = ((Booking)selecteditem).Price.ToString();
            edit.ConferenceRoomIdTextBox.Text = ((Booking)selecteditem).Room.Name;
            edit.CustomerCVRTextBox.Text = ((Booking)selecteditem).Customer.Name;

            edit.ShowDialog();
            ICollectionView view = CollectionViewSource.GetDefaultView(Bookings);
            view.Refresh();
        }

        private void BookingOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            Overview.Gui.RoomOverviewWindow roomoverview = new Overview.Gui.RoomOverviewWindow();
            roomoverview.Show();
        }

        private void DeleteBookingButton_Click(object sender, RoutedEventArgs e)
        {
            bookingCrud.Delete((Booking)BookingList.SelectedItems[0]);
            Bookings.Remove((Booking)BookingList.SelectedItems[0]);            
        }

        private void BookingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookingList.SelectedItem != null)
                EditBookingButton.IsEnabled = true;
            else
                EditBookingButton.IsEnabled = false;
        }
    }
}

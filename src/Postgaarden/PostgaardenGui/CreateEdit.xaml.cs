using Postgaarden;
using Postgaarden.Connection.Sqlite;
using Postgaarden.Crud.Equipments;
using Postgaarden.Crud.Persons;
using Postgaarden.Crud.Rooms;
using Postgaarden.Model.Bookings;
using Postgaarden.Model.Rooms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Xceed.Wpf.Toolkit;

namespace PostgaardenGui
{
/*
    Developed by Christoffer Stenshøj
*/
    /// <summary>
    /// Interaction logic for Create_Edit.xaml
    /// </summary>
    public partial class CreateEdit : Window
    {
        private EmployeeCrud empCrud;
        private CustomerCrud cusCrud;
        private EquipmentCrud equiCrud;
        private RoomCrud roomCrud;
        private BookingCrud bookingCrud;
        BookingHandler bookingHandler;
        public ObservableCollection<Booking> Bookings { get; set; }
        public ObservableCollection<string> EquipmentFilter { get; set; }
        public ObservableCollection<Room> AvailableRooms { get; set; }
        public Booking Booking { get; set; }
        public string createEdit { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEdit"/> class.
        /// </summary>
        /// <param name="createEdit">The headline for the CreateEdit window</param>
        /// <param name="bookings">The bookings.</param>
        /// <param name="booking">The booking.</param>
        public CreateEdit(string createEdit, ObservableCollection<Booking> bookings, Booking booking = null)
        {
            EquipmentFilter = new ObservableCollection<string>();
            AvailableRooms = new ObservableCollection<Room>();

            InitializeComponent();

            string filePath = Properties.Settings.Default.Postgaarden;
            var sqliteInstance = SqliteDatabaseConnection.GetInstance(filePath);

            empCrud = new SqliteEmployeeCrud(sqliteInstance);
            cusCrud = new SqliteCustomerCrud(sqliteInstance);
            equiCrud = new SqliteEquipmentCrud(sqliteInstance);
            roomCrud = new SqliteRoomCrud(sqliteInstance, equiCrud);
            bookingCrud = new SqliteBookingCrud(sqliteInstance, roomCrud, cusCrud, empCrud);
            bookingHandler = new BookingHandler(bookingCrud, new RoomHandler(roomCrud));

            this.createEdit = createEdit;
            Bookings = bookings;
            if (booking != null)
                Booking = booking;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (createEdit.ToUpper())
            {
                case "CREATE":
                    CreateEditHeadline.Text = "Lav ny booking";
                    break;
                case "EDIT":
                    CreateEditHeadline.Text = "Opdater en booking";
                    
                    break;
                                                        
            }
        }
        /// <summary>
        /// Handles the Click event of the OKButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            switch (createEdit.ToUpper())
            {
                case "CREATE":
                    Booking booking = new Booking();
                    if (!booking.SetTime(Convert.ToDateTime(StartTimePicker.Text), Convert.ToDateTime(EndTimePicker.Text)))
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Sluttidspunktet skal være senere end starttidspunktet.");
                        return;
                    }
                    booking.Room = roomCrud.Read(((Room)RoomComboBox.SelectedItem).Id);
                    booking.Customer = cusCrud.Read(CustomerCVRTextBox.Text);
                    booking.Employee = empCrud.Read(Convert.ToInt32(EmployeeIdTextBox.Text));
                    booking.Price = Convert.ToDouble(PriceTextBox.Text);
                    bookingCrud.Create(booking);
                    Bookings.Add(booking);
                    break;
                case "EDIT":
                    booking = Booking;
                    if (!booking.SetTime(Convert.ToDateTime(StartTimePicker.Text), Convert.ToDateTime(EndTimePicker.Text)))
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Sluttidspunktet skal være senere end starttidspunktet.");
                        return;
                    }
                    booking.Room = roomCrud.Read(Convert.ToInt32(RoomComboBox.Text));
                    booking.Customer = cusCrud.Read(CustomerCVRTextBox.Text);
                    booking.Employee = empCrud.Read(Convert.ToInt32(EmployeeIdTextBox.Text));
                    booking.Price = Convert.ToDouble(PriceTextBox.Text);
                    bookingCrud.Update(booking);

                    break;
            }
           
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region Developed By Chris Wohlert

        private void AddEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
            string item = EquipmentTextBox.Text;
            if (!EquipmentFilter.Contains(item))
            {
                EquipmentFilter.Add(item);
                UpdateAvailableRooms();
            }
        }

        private void RemoveEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (EquipmentFilterListBox.SelectedItems.Count == 0) return;
            string item = EquipmentFilterListBox.SelectedItems[0].ToString();
            if (EquipmentFilter.Contains(item))
            {
                EquipmentFilter.Remove(item);
                UpdateAvailableRooms();
            }
        }

        private void SizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAvailableRooms();
        }

        private void UpdateAvailableRooms()
        {
            IEnumerable<Room> rooms;
            int size;
            if (SizeTextBox.Text.Equals(""))
                size = 0;
            else
                size = Convert.ToInt32(SizeTextBox.Text);

            if (createEdit.ToUpper().Equals("CREATE"))
                rooms = bookingHandler.GetAvailableRooms(
                    StartTimePicker.Value ?? DateTime.Now, 
                    EndTimePicker.Value ?? DateTime.Now, 
                    size,
                    EquipmentFilter
                    ).ToList();
            else
                rooms = bookingHandler.GetAvailableRooms(
                    Booking,
                    StartTimePicker.Value ?? DateTime.Now,
                    EndTimePicker.Value ?? DateTime.Now,
                    size,
                    EquipmentFilter
                    ).ToList();


            AvailableRooms.Clear();
            foreach (var room in rooms)
                AvailableRooms.Add(room);

        }

        #endregion

        private void StartTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UpdateAvailableRooms();
        }

        private void EndTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UpdateAvailableRooms();
        }
    }
}

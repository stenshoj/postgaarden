using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public ObservableCollection<Booking> Bookings {get; set;} = new ObservableCollection<Booking>();
        private BookingCrud bookingCrud = new BookingCrud();
        public MainWindow()
        {           
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Bookings = new ObservableCollection<Booking>(bookingCrud.Read());            
            BookingList.ItemsSource = Bookings;
        }

        private void CreateBookingButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEdit create = new CreateEdit("Create");
            create.ShowDialog();
        }

        private void EditBookingButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEdit edit = new CreateEdit("Edit");
            var selecteditem = (dynamic)BookingList.SelectedItems[0];
            edit.StartTimeTextBox.Text = selecteditem.StartTime.ToString();
            edit.EndTimeTextBox.Text = selecteditem.EndTime.ToString();
            edit.EmployeeIdTextBox.Text = selecteditem.EmployeeId.ToString();
            edit.PriceTextBox.Text = selecteditem.Price.ToString();
            edit.ConferenceRoomIdTextBox.Text = selecteditem.ConferenceRoomId.ToString();
            edit.CustomerCVRTextBox.Text = selecteditem.CustomerCVR.ToString();

            edit.ShowDialog();
        }

        private void BookingOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            Overview.Gui.RoomOverviewWindow roomoverview = new Overview.Gui.RoomOverviewWindow();
            roomoverview.Show();
        }

        private void DeleteBookingButton_Click(object sender, RoutedEventArgs e)
        {            
        }
    }
}

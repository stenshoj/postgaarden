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
    /// Made by Christoffer
    /// <summary>
    /// Interaction logic for Create_Edit.xaml
    /// </summary>
    public partial class CreateEdit : Window
    {
        private BookingCrud bookingCrud = new BookingCrud();
        public ObservableCollection<Booking> Bookings { get; set; }
        public Booking Booking { get; set; }
        public string createEdit { get; set; }
        public CreateEdit(string createEdit, ObservableCollection<Booking> bookings, Booking booking = null)
        {
            InitializeComponent();
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

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            switch (createEdit.ToUpper())
            {
                case "CREATE":
                    Booking booking = new Booking();
                    booking.StartTime = Convert.ToDateTime(StartTimePicker.Text);
                    booking.EndTime = Convert.ToDateTime(EndTimeTextBox.Text);
                    booking.ConferenceRoomId = Convert.ToInt32(ConferenceRoomIdTextBox.Text);
                    booking.CustomerCVR = CustomerCVRTextBox.Text;
                    booking.EmployeeId = Convert.ToInt32(EmployeeIdTextBox.Text);
                    booking.Price = Convert.ToDouble(PriceTextBox.Text);
                    bookingCrud.Create(new Booking());
                    Bookings.Add(booking);
                    break;
                case "EDIT":
                    booking = Booking;
                    booking.StartTime = Convert.ToDateTime(StartTimePicker.Text);
                    booking.EndTime = Convert.ToDateTime(EndTimeTextBox.Text);
                    booking.ConferenceRoomId = Convert.ToInt32(ConferenceRoomIdTextBox.Text);
                    booking.CustomerCVR = CustomerCVRTextBox.Text;
                    booking.EmployeeId = Convert.ToInt32(EmployeeIdTextBox.Text);
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
    }
}

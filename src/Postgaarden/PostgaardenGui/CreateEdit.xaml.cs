using System;
using System.Collections.Generic;
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

namespace PostgaardenGui
{
    /// <summary>
    /// Interaction logic for Create_Edit.xaml
    /// </summary>
    public partial class CreateEdit : Window
    {
        private BookingCrud bookingCrud = new BookingCrud();
        private MainWindow main = new MainWindow();
        public string createEdit { get; set; }
        public CreateEdit(string createEdit)
        {
            InitializeComponent();
            this.createEdit = createEdit;       
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
            Booking booking = new Booking();
            booking.StartTime = Convert.ToDateTime(StartTimeTextBox.Text);
            booking.EndTime = Convert.ToDateTime(EndTimeTextBox.Text);
            booking.ConferenceRoomId = Convert.ToInt32(ConferenceRoomIdTextBox.Text);
            booking.CustomerCVR = CustomerCVRTextBox.Text;
            booking.EmployeeId = Convert.ToInt32(EmployeeIdTextBox.Text);
            booking.Price = Convert.ToDouble(PriceTextBox.Text);
            switch (createEdit.ToUpper())
            {
                case "CREATE":
                    bookingCrud.Create(new Booking());
                    break;
                case "EDIT":
                    bookingCrud.Update(new Booking());
                    break;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

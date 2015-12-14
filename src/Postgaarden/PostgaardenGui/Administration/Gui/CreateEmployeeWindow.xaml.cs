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
using Postgaarden.Model.Persons;

namespace PostgaardenGui.Administration.Gui
{
    /// <summary>
    /// Interaction logic for CreateEmployeeWindow1.xaml
    /// </summary>
    public partial class CreateEmployeeWindow : Window
    {
        public CreateEmployeeWindow(Employee employee = null)
        {
            InitializeComponent();

            NewEmployee = employee ?? new Employee();
            
            DataContext = this;
        }

        public Employee NewEmployee { get; set; }
        public Employee Employee { get; set; }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            Employee = NewEmployee;
            DialogResult = true;
        }
    }
}

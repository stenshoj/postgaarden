using System;
using System.Collections;
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

namespace PostgaardenGui.Overview.Gui
{
    /// <summary>
    /// Interaction logic for RoomOverviewWindow.xaml
    /// </summary>
    public partial class RoomOverviewWindow : Window
    {
        //RoomHandler roomHandler = new RoomHandler();

        public ObservableCollection<string> EquipmentFiltlerObservableCollection { get; set; } = new ObservableCollection<string>();
        //public ObservableCollection<Room> RoomObservableCollection { get; set; } = new ObservableCollection<Room>();

        public RoomOverviewWindow()
        {
            InitializeComponent();
        }

        private void RoomOverviewWindow_OnLoaded(object sender, RoutedEventArgs e)
        {

        }
        

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
            var size = SetSizeTextBox.Text.Equals("") ? "0" : SetSizeTextBox.Text;
            //var sizeRooms = new List<Room>(roomHandler.Filter(Convert.ToInt32(size), SetMinimumCheckBox.IsChecked ?? true));
            //var equipmentRooms = new List<Room>(roomHandler.Filter(EquipmentFiltlerObservableCollection));
            //RoomObservableCollection = new ObservableCollection<Room>(sizeRooms.Intersect(equipmentRooms));
            //RoomListBox.ItemsSource = RoomObservableCollection;
        }
    }
}

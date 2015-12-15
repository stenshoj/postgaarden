using Postgaarden.Model.Rooms;
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
using Postgaarden.Connection.Sqlite;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Postgaarden.Crud.Rooms;
using Postgaarden.Crud.Equipments;
using System.Diagnostics;

namespace PostgaardenGui.Overview.Gui
{
    /*
        Developed by Martin Hansen
    */

    /// <summary>
    /// Interaction logic for RoomOverviewWindow.xaml
    /// </summary>
    public partial class RoomOverviewWindow : Window
    {
        RoomHandler roomHandler;

        public ObservableCollection<string> EquipmentFiltlerObservableCollection { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Room> RoomObservableCollection { get; set; } = new ObservableCollection<Room>();

        public RoomOverviewWindow()
        {
            InitializeComponent();

            var connection = SqliteDatabaseConnection.GetInstance(Properties.Settings.Default.Postgaarden);
            var equipmentCrud = new SqliteEquipmentCrud(connection);
            var roomCrud = new SqliteRoomCrud(connection, equipmentCrud);
            roomHandler = new RoomHandler(roomCrud);
        }

        private void RoomOverviewWindow_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the OnClick event of the AddFilterButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void AddFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            EquipmentFiltlerObservableCollection.Add(FilterTextTextBox.Text);
            FilterTextTextBox.Text = "";
        }

        /// <summary>
        /// Handles the OnClick event of the RemoveFilterButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void RemoveFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            EquipmentFiltlerObservableCollection.Remove((string)EquipmentFilterListBox.SelectedItem);
        }

        /// <summary>
        /// Handles the OnTextChanged event of the FilterTextTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs" /> instance containing the event data.</param>
        private void FilterTextTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            AddFilterButton.IsEnabled = FilterTextTextBox.Text != "";
        }

        /// <summary>
        /// Handles the OnSelectionChanged event of the EquipmentFilterListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void EquipmentFilterListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveFilterButton.IsEnabled = true;
        }

        /// <summary>
        /// Handles the OnClick event of the SearchEquipmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
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
                System.Windows.MessageBox.Show("Kontrollér venligst de indtastede søgekriterier", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

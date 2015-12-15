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
using Postgaarden.Connection.Sqlite;
using Postgaarden.Crud.Equipments;
using Postgaarden.Model.Equipments;
using Postgaarden.Model.Rooms;

namespace PostgaardenGui.Administration.Gui
{
    /*
        Developed by Martin Hansen
    */

    /// <summary>
    /// Interaction logic for CreateRoomWindow.xaml
    /// </summary>
    public partial class CreateRoomWindow : Window
    {
        public CreateRoomWindow(ConferenceRoom conferenceRoom = null)
        {
            InitializeComponent();

            var connection = SqliteDatabaseConnection.GetInstance(Properties.Settings.Default.Postgaarden);
            equipmentCrud = new SqliteEquipmentCrud(connection);
            NewConferenceRoom = conferenceRoom ?? new ConferenceRoom();

            DataContext = this;
        }

        private readonly SqliteEquipmentCrud equipmentCrud;
        public ConferenceRoom ConferenceRoom { get; set; }
        public ConferenceRoom NewConferenceRoom { get; set; }
        public ObservableCollection<Equipment> Equipments { get; set; }

        /// <summary>
        /// Handles the OnLoaded event of the CreateRoomWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CreateRoomWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Equipments = new ObservableCollection<Equipment>(equipmentCrud.Read());

            EquipmentListView.ItemsSource = Equipments;
        }

        /// <summary>
        /// Handles the OnClick event of the OkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            var items = EquipmentListView.SelectedItems;
            NewConferenceRoom.Equipments.Clear();

            foreach (var item in items)
            {
                equipmentCrud.Update((Equipment)item, NewConferenceRoom);
                NewConferenceRoom.Equipments.Add((Equipment)item);
            }

            ConferenceRoom = NewConferenceRoom;
            DialogResult = true;
        }
    }
}

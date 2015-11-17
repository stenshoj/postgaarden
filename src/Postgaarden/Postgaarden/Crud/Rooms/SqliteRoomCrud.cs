using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postgaarden.Connection;
using Postgaarden.Model.Rooms;
using Postgaarden.Connection.Sqlite;
using Postgaarden.Model.Equipments;
using Postgaarden.Crud.Equipments;

namespace Postgaarden.Crud.Rooms
{
    /*
        Developed by Chris Wohlert
    */
    public class SqliteRoomCrud : RoomCrud
    {
        /// <summary>
        /// The rooms
        /// </summary>
        private List<Room> rooms = new List<Room> {
                new ConferenceRoom { Id = 1, Size = 8 },
                new ConferenceRoom { Id = 2, Size = 4 },
                new ConferenceRoom { Id = 3, Size = 8 },
                new ConferenceRoom { Id = 4, Size = 12 },
                new ConferenceRoom { Id = 5, Size = 6 },
                new ConferenceRoom { Id = 6, Size = 6 },
            };


        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteRoomCrud"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public SqliteRoomCrud(DatabaseConnection connection)
        {
            this.DBConnection = connection;
        }

        /// <summary>
        /// Creates the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public override void Create(Room entry)
        {
            rooms.Add(entry);
        }

        /// <summary>
        /// Deletes the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public override void Delete(Room entry)
        {
            rooms.Remove(entry);
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns>
        /// Returns every T from the database connection
        /// </returns>
        public override IEnumerable<Room> Read()
        {
            var equipmentCrud = new SqliteEquipmentCrud();

            foreach (var room in rooms)
                foreach (var e in equipmentCrud.Read(room))
                    room.Equipments.Add(e);

            return rooms;


        //    var items = DBConnection.ExecuteQuery("ewq");

        //    foreach (var row in items)
        //    {
        //        Room room = new ConferenceRoom();
        //        room.Id = (int)row.ElementAt(0);
        //        room.Size = (int)row.ElementAt(1);
        //        foreach(var e in equipmentCrud.Read(room))
        //        {
        //            room.Equipments.Add(e);
        //        }
        //        yield return room;
        //    }
        }

        public override Room Read(Booking booking)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>
        /// Returns every T from the database connection
        /// </returns>
        public override Room Read(int key)
        {
            var cols = DBConnection.ExecuteQuery("ewq");
            return new ConferenceRoom { Id = (int)cols.First().First(), Size = (int)cols.First().ElementAt(1) };
        }

        /// <summary>
        /// Updates the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public override void Update(Room entry)
        {
            DBConnection.ExecuteQuery(string.Format($"UPDATE Room SET Size = {entry.Size} WHERE Id = {entry.Id}"));
        }
    }
}

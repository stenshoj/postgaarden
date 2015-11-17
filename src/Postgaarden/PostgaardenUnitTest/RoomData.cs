using Postgaarden.Model.Equipments;
using Postgaarden.Model.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgaardenUnitTest
{
    /*
        Developed by Chris Wohlert
    */
    public class RoomData
    {
        /// <summary>
        /// The rooms
        /// </summary>
        public List<Room> Rooms = new List<Room> {
                new ConferenceRoom { Id = 1, Size = 8 },
                new ConferenceRoom { Id = 2, Size = 4 },
                new ConferenceRoom { Id = 3, Size = 8 },
                new ConferenceRoom { Id = 4, Size = 12 },
                new ConferenceRoom { Id = 5, Size = 6 },
                new ConferenceRoom { Id = 6, Size = 6 },
            };

        /// <summary>
        /// The equipments
        /// </summary>
        public Dictionary<Equipment, int> equipments;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomData"/> class.
        /// </summary>
        public RoomData()
        {
            equipments = new Dictionary<Equipment, int>();
            equipments[new Equipment("Kaffemaskiner") { Id = 1 }] = 1;
            equipments[new Equipment("Tavle") { Id = 2 }] = 1;
            equipments[new Equipment("Projektor") { Id = 3 }] = 1;
            equipments[new Equipment("Stol") { Id = 4 }] = 1;
            equipments[new Equipment("Bord") { Id = 5 }] = 2;
            equipments[new Equipment("Stol") { Id = 6 }] = 2;
            equipments[new Equipment("Projektor") { Id = 7 }] = 2;
            equipments[new Equipment("Stol") { Id = 8 }] = 3;
            equipments[new Equipment("Bord") { Id = 9 }] = 3;
            equipments[new Equipment("Kaffemaskine") { Id = 10 }] = 3;
            equipments[new Equipment("Bord") { Id = 11 }] = 3;
            equipments[new Equipment("Stol") { Id = 12 }] = 4;
            equipments[new Equipment("Projektor") { Id = 13 }] = 4;
            equipments[new Equipment("Stol") { Id = 14 }] = 4;
            equipments[new Equipment("Kaffemaskine") { Id = 3 }] = 4;
            equipments[new Equipment("Projektor") { Id = 7 }] = 4;

            foreach (var room in Rooms)
                foreach (var e in equipments)
                    if (e.Value == room.Id) room.Equipments.Add(e.Key);
        }

        /// <summary>
        /// Gets the room without equipment.
        /// </summary>
        /// <returns></returns>
        public object[][] GetRoomAsObjectArray()
        {
            return new object[][] {
                new [] { "1", "8" },
                new [] { "2", "4" },
                new [] { "3", "8" },
                new [] { "4", "12" },
                new [] { "5", "6" },
                new [] { "6", "6" }
            };
        }

        /// <summary>
        /// Gets the equipment by room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns></returns>
        public IEnumerable<Equipment> GetEquipmentByRoom(Room room)
        {
            return equipments.Where(e => e.Value == room.Id).Select(key => key.Key);
        }
    }
}

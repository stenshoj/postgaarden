using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postgaarden.Model.Equipments;
using Postgaarden.Model.Rooms;

namespace Postgaarden.Crud.Equipments
{
    /*
        Developed by Chris Wohlert
    */
    class SqliteEquipmentCrud : EquipmentCrud
    {
        private Dictionary<Equipment, int> equipments;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteEquipmentCrud"/> class.
        /// </summary>
        public SqliteEquipmentCrud()
        {
            equipments = new Dictionary<Equipment, int>();
            equipments[new Equipment("Kaffemaskine") { Id = 1 }] = 1;
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
            equipments[new Equipment("Stol") { Id = 8 }] = 4;
            equipments[new Equipment("Bord") { Id = 9 }] = 5;
            equipments[new Equipment("Tavle") { Id = 10 }] = 5;
            equipments[new Equipment("Stol") { Id = 4 }] = 5;
            equipments[new Equipment("Bord") { Id = 5 }] = 5;
            equipments[new Equipment("Projektor") { Id = 13 }] = 5;
            equipments[new Equipment("Kaffemaskine") { Id = 14 }] = 1;
            equipments[new Equipment("Tavle") { Id = 3 }] = 2;
            equipments[new Equipment("Projektor") { Id = 7 }] = 3;
            equipments[new Equipment("Stol") { Id = 8 }] = 4;
            equipments[new Equipment("Bord") { Id = 5 }] = 5;
            equipments[new Equipment("Tavle") { Id = 13 }] = 6;
            equipments[new Equipment("Stol") { Id = 14 }] = 6;
            equipments[new Equipment("Projektor") { Id = 7 }] = 6;
            equipments[new Equipment("Stol") { Id = 8 }] = 6;
            equipments[new Equipment("Bord") { Id = 9 }] = 6;
        }

        /// <summary>
        /// Creates the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public override void Create(Equipment entry)
        {

        }

        /// <summary>
        /// Deletes the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public override void Delete(Equipment entry)
        {

        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns>
        /// Returns every T from the database connection
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<Equipment> Read()
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
        /// <exception cref="System.NotImplementedException"></exception>
        public override Equipment Read(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns></returns>
        public IEnumerable<Equipment> Read(Room room)
        {
            foreach (var e in equipments)
                if (e.Value == room.Id) yield return e.Key;
        }

        /// <summary>
        /// Updates the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public override void Update(Equipment entry)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postgaarden.Model.Equipments;
using Postgaarden.Model.Rooms;
using Postgaarden.Connection;

namespace Postgaarden.Crud.Equipments
{
    /*
        Developed by Chris Wohlert
    */
    public class SqliteEquipmentCrud : EquipmentCrud
    {
        private Dictionary<Equipment, int> equipments;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteEquipmentCrud"/> class.
        /// </summary>
        public SqliteEquipmentCrud(DatabaseConnection connection)
        {
            this.DBConnection = connection;
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
        public override IEnumerable<Equipment> Read(Room room)
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

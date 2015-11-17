using Postgaarden.Model.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgaarden.Model.Rooms
{
    /*
        Chris Wohlert
    */
    public abstract class Room
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public abstract String Name{ get; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size { get; set; }

        /// <summary>
        /// Gets the equipments.
        /// </summary>
        /// <value>
        /// The equipments.
        /// </value>
        public List<Equipment> Equipments { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        protected Room()
        {
            Equipments = new List<Equipment>();
        }
    }
}

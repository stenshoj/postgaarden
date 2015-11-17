using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgaarden.Model.Rooms
{
    /*
        Developed by Chris Wohlert
    */
    public class ConferenceRoom : Room
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name
        {
            get
            {
                return "Mødelokale " + this.Id;
            }
        }
    }
}

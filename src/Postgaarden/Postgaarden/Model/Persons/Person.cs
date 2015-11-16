using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgaarden.Model.Persons
{
    public abstract class Person
    {
        private string name;
        private string emailaddress;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        public string EmailAddress
        {
            get
            {
                return emailaddress;
            }
            set
            {
                emailaddress = value;
            }
        }
    }
}

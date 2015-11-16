using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgaarden.Model.Persons
{
    public class Customer : Person
    {
        private string cvr;
        private string companyName;

        public string Cvr
        {
            get
            {
                return cvr;
            }
            set
            {
                cvr = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }

    }
}


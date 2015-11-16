using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgaarden
{
    public class DatabaseConnection
    {
        public virtual IEnumerable<IEnumerable<object>> ExecuteQuery(string query)
        {
            return new object[][] { new[] { "From the DatabaseConnection" } };
        }
    }
}

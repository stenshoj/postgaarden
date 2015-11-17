using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgaarden.Connection.Sqlite
{
    /*
        Developed by Chris Wohlert
    */
    public class SqliteDatabaseConnection : DatabaseConnection
    {
        private static DatabaseConnection instance;
        private SQLiteConnection sqlConnection;

        /// <summary>
        /// Prevents a default instance of the SqliteDatabaseConnection class from being created.
        /// </summary>
        private SqliteDatabaseConnection()
        {
            sqlConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.SqliteConnection + ";Version=3;");
        }

        /// <summary>
        /// Opens this instance.
        /// </summary>
        protected override void Open()
        {
            sqlConnection.Open();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        protected override void Close()
        {
            sqlConnection.Close();
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        public override IEnumerable<IEnumerable<object>> ExecuteQuery(string sql)
        {
            Open();

            var command = new SQLiteCommand(sql, sqlConnection);
            var reader = command.ExecuteReader();
            var sqlReturn = Read(reader);

            Close();

            return sqlReturn;
        }

        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private IEnumerable<IEnumerable<object>> Read(SQLiteDataReader reader)
        {
            while (reader.Read())
            {
                var row = new object[reader.FieldCount];
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    row[i] = reader.GetValue(i);
                    if (reader.IsDBNull(i))
                        row[i] = "";
                }
                yield return row;
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static DatabaseConnection GetInstance()
        {
            if (instance == null) instance = new SqliteDatabaseConnection();
            return instance;
        }
    }
}

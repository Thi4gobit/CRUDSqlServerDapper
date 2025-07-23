using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDSqlServerDapper.Settings
{
    /// <summary>
    /// Represents application settings for the CRUD SQL Server Dapper application.
    /// </summary>
    public class AppSettings
    {
        public string ConnectionString 
        {
            /// <summary>
            /// Gets the connection string for the SQL Server database.
            /// </summary>
            get
            {
                return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBClients;Integrated Security=True;";
            }
        }
    }
}

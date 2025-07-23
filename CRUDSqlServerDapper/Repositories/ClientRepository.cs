using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDSqlServerDapper.Entities;
using CRUDSqlServerDapper.Settings;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CRUDSqlServerDapper.Repositories
{
    /// <summary>
    /// Represents a repository for managing client entities in the CRUD SQL Server Dapper application.
    /// </summary>
    public class ClientRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRepository"/> class.
        /// </summary>
        private AppSettings _appSettings = new AppSettings();

        /// <summary>
        /// Inserts a new client into the database.
        /// </summary>
        public void Insert(Client client)
        {
            var query = @"
                    INSERT INTO CLIENT (ID, NAME, EMAIL, BIRTHDATE)
                    VALUES(@Id, @Name, @Email, @BirthDate)
            ";

            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                connection.Execute(query, client);
            }
        }
    }
}

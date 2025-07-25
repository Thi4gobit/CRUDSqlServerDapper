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


        /// <summary>
        /// Updates an existing client in the database.
        /// </summary> 
        public void Update(Client client)
        {
            var query = @"
                    UPDATE CLIENT
                    SET 
                        NAME = @Name, 
                        EMAIL = @Email, 
                        BIRTHDATE = @BirthDate
                    WHERE 
                        ID = @Id
            ";
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                connection.Execute(query, client);
            }
        }

        /// <summary>
        /// Deletes a client from the database by its ID.
        /// </summary>
        public void Delete(Guid id)
        {
            var query = @"
                    DELETE FROM CLIENT
                    WHERE ID = @Id
            ";
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                connection.Execute(query, new { Id = id });
            }
        }

        /// <summary>
        /// Retrieves all clients from the database.
        /// </summary>
        public List<Client> GetAll()
        {
            //Escrevendo o comando SQL
            var query = @"
                SELECT ID, NAME, EMAIL, BIRTHDATE
                FROM CLIENT
                ORDER BY NAME
            ";

            //Conectando no banco de dados
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                return connection.Query<Client>(query).ToList();
            }
        }

        /// <summary>
        /// Retrieves a client by its ID from the database.
        /// </summary>
        public Client? GetById(Guid id)
        {
            var query = @"
                SELECT ID, NAME, EMAIL, BIRTHDATE
                FROM CLIENT
                WHERE ID = @Id
            ";

            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                return connection.QueryFirstOrDefault<Client>(query, new { Id = id });
            }
        }
    }
}

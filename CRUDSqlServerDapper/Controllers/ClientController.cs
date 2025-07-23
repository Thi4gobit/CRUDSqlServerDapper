using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDSqlServerDapper.Entities;
using CRUDSqlServerDapper.Repositories;
using CRUDSqlServerDapper.Validators;

namespace CRUDSqlServerDapper.Controllers
{
    /// <summary>
    /// Represents a controller for managing client entities in the CRUD SQL Server Dapper application.
    /// </summary>
    public class ClientController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("\nSISTEMA DE CONTROLE DE CLIENTES:\n");
            Console.WriteLine("(1) - CADASTRAR CLIENTE");
            Console.WriteLine("(2) - ATUALIZAR CLIENTE");
            Console.WriteLine("(3) - EXCLUIR CLIENTE");
            Console.WriteLine("(4) - PESQUISAR CLIENTE");
            Console.Write("\nINFORME A OPÇÃO DESEJADA...: ");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    CreateClient();
                    break;

                default:
                    Console.WriteLine("OPÇÃO INVÁLIDA!");
                    break;
            }

            Console.Write("\nDESEJA CONTINUAR? (S,N):");
            var go = Console.ReadLine() ?? string.Empty;

            if (go.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear();
                Execute();
            }
            else
            {
                Console.WriteLine("\nFIM DO PROGRAMA!");
            }
        }

        public void CreateClient()
        {
            Console.WriteLine("\nCADASTRO DE CLIENTE:\n");

            var client = new Client();

            Console.Write("INFORME O NOME.................: ");
            client.Name = Console.ReadLine() ?? string.Empty;

            Console.Write("INFORME O EMAIL................: ");
            client.Email = Console.ReadLine() ?? string.Empty;

            Console.Write("INFORME A DATA DE NASCIMENTO...: ");
            client.BirthDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

            var validator = new ClientValidator();
            var validationResult = validator.Validate(client);

            if (!validationResult.IsValid)
            {
                Console.WriteLine("\nERROS DE VALIDAÇÃO:\n");
                foreach (var error in validationResult.Errors)
                {
                    Console.WriteLine($"Erro: {error.ErrorMessage}");
                }
                return;
            }

            var clientRepository = new ClientRepository();
            clientRepository.Insert(client);

            Console.WriteLine("\nCLIENTE CADASTRADO COM SUCESSO!\n");
        }
    }
}

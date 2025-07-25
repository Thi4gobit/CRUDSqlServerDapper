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

                case "2": //caso a opção seja "4"
                    UpdateClient(); //executando o método para consultar os clientes
                    break;

                case "3": //caso a opção seja "4"
                    DeleteClient(); //executando o método para consultar os clientes
                    break;

                case "4": //caso a opção seja "4"
                    ReadClients(); //executando o método para consultar os clientes
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

        /// <summary>
        /// Método para realizar a consulta dos clientes
        /// </summary>
        private void ReadClients()
        {
            Console.WriteLine("\nCONSULTA DE CLIENTES:\n");

            //Criando um objeto da classe de repositório
            var clientRepository = new ClientRepository();

            //consultar e obter uma lista de clientes
            var clients = clientRepository.GetAll();

            //exibir cada cliente obtido do banco de dados
            foreach (var item in clients)
            {
                Console.WriteLine($"ID..........: {item.Id}");
                Console.WriteLine($"NAME........: {item.Name}");
                Console.WriteLine($"EMAIL.......: {item.Email}");
                Console.WriteLine($"BIRTHDATE...: {item.BirthDate}");
                Console.WriteLine("...");
            }
        }

        private void UpdateClient()
        {
            Console.WriteLine("\nEDIÇÃO DE CLIENTES:\n");

            Console.WriteLine($"INFORME O ID DO CLIENTE..........: ");
            var id = Guid.Parse(Console.ReadLine() ?? string.Empty);

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(id);

            if (client == null)
            {
                Console.WriteLine("\nCLIENTE NÃO ENCONTRADO!\n");
                return;
            }

            Console.WriteLine("\nDADOS DO CLIENTE:\n");

            Console.WriteLine($"ID..........: {client.Id}");
            Console.WriteLine($"NAME........: {client.Name}");
            Console.WriteLine($"EMAIL.......: {client.Email}");
            Console.WriteLine($"BIRTHDATE...: {client.BirthDate}");

            Console.WriteLine("\nINFORME OS DADOS PARA EDIÇÃO:\n");

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

            clientRepository.Update(client);

            Console.WriteLine("\nCLIENTE ATUALIZADO COM SUCESSO!\n");
        }

        private void DeleteClient()
        {
            Console.WriteLine("\nEXCLUSÃO DE CLIENTES:\n");

            Console.WriteLine($"INFORME O ID DO CLIENTE..........: ");
            var id = Guid.Parse(Console.ReadLine() ?? string.Empty);

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(id);

            if (client == null)
            {
                Console.WriteLine("\nCLIENTE NÃO ENCONTRADO!\n");
                return;
            }

            Console.WriteLine("\nDADOS DO CLIENTE:\n");

            Console.WriteLine($"ID..........: {client.Id}");
            Console.WriteLine($"NAME........: {client.Name}");
            Console.WriteLine($"EMAIL.......: {client.Email}");
            Console.WriteLine($"BIRTHDATE...: {client.BirthDate}");

            Console.WriteLine("\nDESEJA EXCLUIR O CLIENTE? (S,N): ");

            var opcao = Console.ReadLine() ?? string.Empty;

            if(!opcao.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nEXCLUSÃO CANCELADA!\n");
                return;
            }

            clientRepository.Delete(id);

            Console.WriteLine("\nCLIENTE DELETEDO COM SUCESSO!\n");
        }


    }
}

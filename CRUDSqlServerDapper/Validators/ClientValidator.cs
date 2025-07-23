using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDSqlServerDapper.Entities;
using FluentValidation;

namespace CRUDSqlServerDapper.Validators
{
    /// <summary>
    /// Represents a validator for client entities in the CRUD SQL Server Dapper application.
    /// </summary>
    public class ClientValidator : AbstractValidator<Client>
    {
        // Método construtor
        public ClientValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .MinimumLength(6).WithMessage("O nome do cliente deve ter pelo menos 6 caracteres.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O email do cliente é obrigatório.")
                .EmailAddress().WithMessage("O email informado não é válido.");

            RuleFor(c => c.BirthDate)
                .NotEmpty().WithMessage("A data de nascimento do cliente é obrigatória.")
                .LessThan(DateTime.Now).WithMessage("A data de nascimento deve ser anterior à data atual.")
                .GreaterThan(DateTime.Now.AddYears(-120)).WithMessage("A data de nascimento deve ser posterior a 120 anos atrás.");
        }
    }
}

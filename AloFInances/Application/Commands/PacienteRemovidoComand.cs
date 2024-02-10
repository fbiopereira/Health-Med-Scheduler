
using AloDoutor.Core.Messages;
using FluentValidation;

namespace AloFinances.Api.Application.Commands
{
    public class PacienteRemovidoComand : Command
    { 
        public string Cpf { get; private set; }
        public bool Ativo { get; private set; }

        public PacienteRemovidoComand(string cpf)
        {
            Cpf = cpf;           
            Ativo = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverPacienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoverPacienteValidation : AbstractValidator<PacienteRemovidoComand>
    {
        public static string CpfErroMsg => "Cpf do pedido inválido";

        public RemoverPacienteValidation() {           

            RuleFor(c => c.Cpf)
               .NotEmpty()
               .WithMessage(CpfErroMsg);        
            
        }
    }
}

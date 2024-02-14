
using AloDoutor.Core.Messages;
using FluentValidation;

namespace AloFinances.Api.Application.Commands
{
    public class MedicoRemovidoComand : Command
    { 
        public string Cpf { get; private set; }
        public bool Ativo { get; private set; }
        public string Crm { get; private set; }

        public MedicoRemovidoComand(string cpf, string crm)
        {
            Cpf = cpf;
            Ativo = false;
            Crm = crm;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverMedicoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoverMedicoValidation : AbstractValidator<MedicoRemovidoComand>
    {
        public static string CpfErroMsg => "Cpf do pedido inválido";
        public static string CrmErroMsg => "Crm do pedido inválido";

        public RemoverMedicoValidation() {           

            RuleFor(c => c.Cpf)
               .NotEmpty()
               .WithMessage(CpfErroMsg);

            RuleFor(c => c.Crm)
               .NotEmpty()
               .WithMessage(CrmErroMsg);

        }
    }
}

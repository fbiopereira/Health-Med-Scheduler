
using AloDoutor.Core.Messages;
using FluentValidation;

namespace AloFinances.Api.Application.Commands
{
    public class ContaCanceladaComand : Command
    { 
        public Guid IdAgendamento { get; private set; }

        public ContaCanceladaComand(Guid idAgendamento)
        {
            IdAgendamento = idAgendamento;
        }

        public override bool EhValido()
        {
            ValidationResult = new CancelarContaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelarContaValidation : AbstractValidator<ContaCanceladaComand>
    {
        public static string IdAgendamentoErroMsg => "Id Agendamento invalido inválido";

        public CancelarContaValidation() {           

            RuleFor(c => c.IdAgendamento)
               .NotEmpty()
               .WithMessage(IdAgendamentoErroMsg);        
            
        }
    }
}

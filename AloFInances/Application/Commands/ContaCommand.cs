using AloDoutor.Core.Messages;
using FluentValidation;

namespace AloFinances.Api.Application.Commands
{
    public class ContaCommand : Command
    {
        public ContaCommand(Guid idAgendamento, DateTime dataAgendamento, string pacienteCPF, string medicoCrm)
        {
            IdAgendamento = idAgendamento;
            DataAgendamento = dataAgendamento;
            PacienteCPF = pacienteCPF;
            MedicoCrm = medicoCrm;
        }

        public Guid IdAgendamento { get; private set; }
        public DateTime DataAgendamento { get; private set; }
        public string PacienteCPF { get; private set; }
        public string MedicoCrm { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new ContaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ContaValidation : AbstractValidator<ContaCommand>
    {
        public static string IdAgendamentoErroMsg => "IdAgendamento inválido";
        public static string DataAgendamentoErroMsg => "Data Agendamento inválido";
        public static string PacienteCPFErroMsg => "CPF do Paciente inválido";
        public static string MedicoCrmErroMsg => "CRM do médico inválido";

        public ContaValidation()
        {
            RuleFor(c => c.IdAgendamento)
                .NotEmpty()
                .WithMessage(IdAgendamentoErroMsg);

            RuleFor(c => c.DataAgendamento)
               .NotEmpty()
               .WithMessage(DataAgendamentoErroMsg);

            RuleFor(c => c.PacienteCPF)
               .NotEmpty()
               .WithMessage(PacienteCPFErroMsg);

            RuleFor(c => c.MedicoCrm)
               .NotEmpty()
               .WithMessage(MedicoCrmErroMsg);           

        }
    }
}

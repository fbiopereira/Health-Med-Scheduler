using FluentValidation;
using HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarMedico;
using HealthMedScheduler.Domain.Entity;

namespace HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarAgenda
{
    public class AdicionarAgendaMedicoCommandValidator : AbstractValidator<AdicionarAgendaMedicoCommand>
    {
        public AdicionarAgendaMedicoCommandValidator()
        {
            RuleFor(x => x.MedicoId)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.DiaSemana)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                 .Must(dia => dia >= 0 && dia <= 6)
                    .WithMessage("O campo {PropertyName} deve estar entre 0 e 6")
                .Must(dia => dia != (int)DayOfWeek.Saturday && dia != (int)DayOfWeek.Sunday)
                    .WithMessage("O campo {PropertyName} não pode ser sábado ou domingo");

            RuleFor(x => TimeSpan.Parse(x.HoraInicio))
               .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
               .Must(BeAValidTime).WithMessage("O campo {PropertyName} deve estar entre 09:00 e 18:00");

            RuleFor(x => TimeSpan.Parse(x.HoraFim))
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Must(BeAValidTime).WithMessage("O campo {PropertyName} deve estar entre 09:00 e 18:00");

            RuleFor(x => x)
               .Must(HaveValidTimeRange)
               .WithMessage("O horário de início deve ser menor que o horário de fim");
        }
        private bool BeAValidTime(TimeSpan time)
        {
            return time >= new TimeSpan(8, 0, 0) && time <= new TimeSpan(18, 0, 0);
        }

        private bool HaveValidTimeRange(AdicionarAgendaMedicoCommand agenda)
        {
            return TimeSpan.Parse(agenda.HoraInicio) < TimeSpan.Parse(agenda.HoraFim);
        }
    }
}

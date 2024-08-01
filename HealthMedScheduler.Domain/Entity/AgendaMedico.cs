using HealthMedScheduler.Domain.Entity.Common;

namespace HealthMedScheduler.Domain.Entity
{
    public class AgendaMedico : Entidade
    {
        public AgendaMedico(Guid medicoId, DayOfWeek diaSemana, TimeSpan horaInicio, TimeSpan horaFim, Medico medico)
        {
            MedicoId = medicoId;
            DiaSemana = diaSemana;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Medico = medico;
        }

        public AgendaMedico() { }

        public Guid MedicoId { get; private set; }
        public DayOfWeek DiaSemana { get; private set; }
        public TimeSpan HoraInicio { get; private set; }
        public TimeSpan HoraFim { get; private set; }
        public Medico Medico { get; private set; }
    }
}

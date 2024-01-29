using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Domain.Entity
{
    public class Agendamento : Entidade
    {
        public Guid EspecialidadeMedicoId { get; private set; }

        public Guid PacienteId { get; private set; }

        public DateTime DataHoraAtendimento { get; private set; }

        public StatusAgendamento StatusAgendamento { get; private set; }

        /* EF Relations */
        public EspecialidadeMedico EspecialidadeMedico { get; private set; }

        /* EF Relations */
        public Paciente Paciente { get; private set; }

        public Agendamento() { }

        public Agendamento(Guid especialidadeMedicoId, Guid pacienteId, DateTime dataHoraAtendimento)
        {
            EspecialidadeMedicoId = especialidadeMedicoId;
            PacienteId = pacienteId;
            DataHoraAtendimento = dataHoraAtendimento;
            StatusAgendamento = StatusAgendamento.Ativo;
        }

        public void CancelarAgendamento()
        {
            StatusAgendamento = StatusAgendamento.Cancelado;
        }

        public void AlterarDataAgendamento(DateTime data)
        {
            DataHoraAtendimento = data;
        }
    }
}

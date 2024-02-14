using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Core.Messages.Integration
{
    public class AgendamentoRealizadoEvent
    {
        public AgendamentoRealizadoEvent(Guid idAgendamento, DateTime dataAgendamento, string pacienteCPF, string medicoCRM)
        {
            IdAgendamento = idAgendamento;
            DataAgendamento = dataAgendamento;
            PacienteCPF = pacienteCPF;
            MedicoCRM = medicoCRM;
        }

        public Guid IdAgendamento { get; private set; }
        public DateTime DataAgendamento { get; private set; }
        public string PacienteCPF { get; private set; }
        public string MedicoCRM { get; private set; }
    }
}

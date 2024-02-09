using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Core.Messages.Integration
{
    public class AgendamentoRealizadoEvent
    {
        public AgendamentoRealizadoEvent(Guid idAgendamento, DateTime dataAgendamento)
        {
            IdAgendamento = idAgendamento;
            DataAgendamento = dataAgendamento;
        }

        public Guid IdAgendamento { get; private set; }
        public DateTime DataAgendamento { get; private set; }
    }
}

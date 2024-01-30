using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Core.Messages
{
    public class AgendamentoCanceladoEvent
    {
        public AgendamentoCanceladoEvent(Guid idAgendamento)
        {
            IdAgendamento = idAgendamento;            
        }

        public Guid IdAgendamento { get; private set; }       
    }
}

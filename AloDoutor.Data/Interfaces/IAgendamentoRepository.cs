using AloDoutor.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Domain.Interfaces
{
    public interface IAgendamentoRepository :IRepository<Agendamento>
    {
        Task<IEnumerable<Agendamento>> ObterAgendamentosPorIStatus(int status);
    }
}

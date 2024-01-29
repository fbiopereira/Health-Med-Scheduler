using AloDoutor.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Domain.Interfaces
{
    public interface IEspecialidadeMedicoRepository : IRepository<EspecialidadeMedico>
    {
        Task<EspecialidadeMedico> ObterPorIdEspecialidadeIDMedico(Guid idMedico, Guid idEspecialidade);

        Task<bool> VerificarAgendaLivreMedico(Guid idMedido, DateTime dataAtendimento);
    }
}

using AloDoutor.Domain.Entity;

namespace AloDoutor.Domain.Interfaces
{
    public interface IEspecialidadeMedicoRepository : IRepository<EspecialidadeMedico>
    {
        Task<EspecialidadeMedico> ObterPorIdEspecialidadeIDMedico(Guid idMedico, Guid idEspecialidade);

        Task<bool> VerificarAgendaLivreMedico(Guid idMedido, DateTime dataAtendimento);
    }
}

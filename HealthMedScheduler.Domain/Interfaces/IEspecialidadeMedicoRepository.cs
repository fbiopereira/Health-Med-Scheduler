using HealthMedScheduler.Domain.Entity;

namespace HealthMedScheduler.Domain.Interfaces
{
    public interface IEspecialidadeMedicoRepository : IRepository<EspecialidadeMedico>
    {
        Task<EspecialidadeMedico> ObterPorIdEspecialidadeIDMedico(Guid idMedico, Guid idEspecialidade);
        //Task<Medico> ObterMedicoPorIdEspecialidadeIDMedico(Guid idMedico, Guid idEspecialidade);

        Task<bool> VerificarAgendaLivreMedico(Guid idMedido, DateTime dataAtendimento);
    }
}

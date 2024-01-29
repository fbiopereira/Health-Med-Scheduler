using AloDoutor.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Domain.Interfaces
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<Paciente> ObterAgendamentosPorIdPaciente(Guid idPaciente);
        Task<Paciente> ObterPacientePorCPF(string cpf);
        Task<bool> VerificarAgendaLivrePaciente(Guid idPaciente, DateTime dataAtendimento);

        Task<bool> VerificarPacienteCadastrado(Guid idPaciente);
    }
}

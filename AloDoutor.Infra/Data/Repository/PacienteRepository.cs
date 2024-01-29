using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AloDoutor.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Infra.Data.Repository
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(MeuDbContext context) : base(context) { }

        public async Task<Paciente> ObterAgendamentosPorIdPaciente(Guid idPaciente)
        {
            var paciente = await Db.Pacientes
                .Include(p => p.Agendamentos) 
                    .ThenInclude(a => a.EspecialidadeMedico) 
                         .ThenInclude(em => em.Medico) 
                    .ThenInclude(a => a.EspecialidadesMedicos) 
                        .ThenInclude(e => e.Especialidade)  
                .FirstOrDefaultAsync(p => p.Id == idPaciente);

            return paciente;
        }

        public async Task<Paciente> ObterPacientePorCPF(string cpf)
        {
            return await DbSet.FirstOrDefaultAsync<Paciente>(p => p.Cpf == cpf);
        }

        public async Task<bool> VerificarAgendaLivrePaciente(Guid idPaciente, DateTime dataAtendimento)
        {
            var agenda = await DbSet.FirstOrDefaultAsync(e => e.Id == idPaciente && e.Agendamentos.Any(a => a.DataHoraAtendimento.Equals(dataAtendimento) && a.StatusAgendamento == StatusAgendamento.Ativo));

            return agenda == null;
        }

        public async Task<bool> VerificarPacienteCadastrado(Guid idPaciente)
        {
            var paciente = await DbSet.FirstOrDefaultAsync(p => p.Id == idPaciente);

            return paciente != null;
        }
    }
}

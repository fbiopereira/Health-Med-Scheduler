using AloDoutor.Core.Messages;
using AloDoutor.Core.Messages.Integration;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AloDoutor.Domain.Services
{
    public class AgendamentoService : CommandHandler, IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly MassTransit.IBus _bus;
        private readonly ILogger _logger;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository,
            IEspecialidadeMedicoRepository especialidadeMedicoRepository, IPacienteRepository pacienteRepository, MassTransit.IBus bus, IMedicoRepository medicoRepository, ILogger<AgendamentoService> logger)
        {
            _agendamentoRepository = agendamentoRepository;
            _especialidadeMedicoRepository = especialidadeMedicoRepository;
            _pacienteRepository = pacienteRepository;
            _bus = bus;
            _medicoRepository = medicoRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> Adicionar(Agendamento agendamento)
        {
            await ValidarAgendamento(agendamento);

            if (!ValidationResult.IsValid)
                return ValidationResult;   
            
            await _agendamentoRepository.Adicionar(agendamento);

            var commit = await PersistirDados(_agendamentoRepository.UnitOfWork);
            var paciente = await _pacienteRepository.ObterPorId(agendamento.PacienteId);
            var medicoEspecialidade = await _especialidadeMedicoRepository.ObterPorId(agendamento.EspecialidadeMedicoId);
            var medico = await _medicoRepository.ObterPorId(medicoEspecialidade.MedicoId);

            if (commit.IsValid) 
                await _bus.Publish(new AgendamentoRealizadoEvent(agendamento.Id, agendamento.DataHoraAtendimento, paciente.Cpf, medico.Crm));

            return commit;
        }

        public async Task<ValidationResult> Reagendar(Guid id, DateTime data)
        {
            var agendamento = await _agendamentoRepository.ObterPorId(id);
            
            if (agendamento == null)
            {
                AdicionarErro("Agendamento não localizado!");
                return ValidationResult;
            }

            if (agendamento.StatusAgendamento == StatusAgendamento.Cancelado)
            {
                AdicionarErro("Esse agendamento se encontra cancelado!");
                return ValidationResult;
            }            

            if (DateTime.Now.Date.AddDays(CalcularAntecedenciaDiasUteis()) > agendamento.DataHoraAtendimento.Date)
            {
                AdicionarErro("Reagendamento deve ser realizado com pelo menos 2 dias de antecedencia (Desconsiderando sábado e domingo)!");
                return ValidationResult;
            }

            agendamento.AlterarDataAgendamento(data);

            await ValidarAgendamento(agendamento);

            if (!ValidationResult.IsValid)
                return ValidationResult;

            await _agendamentoRepository.Atualizar(agendamento);

            var commit = await PersistirDados(_agendamentoRepository.UnitOfWork);

            var paciente = await _pacienteRepository.ObterPorId(agendamento.PacienteId);
            var medicoEspecialidade = await _especialidadeMedicoRepository.ObterPorId(agendamento.EspecialidadeMedicoId);
            var medico = await _medicoRepository.ObterPorId(medicoEspecialidade.MedicoId);

            if (commit.IsValid)
                await _bus.Publish(new AgendamentoRealizadoEvent(agendamento.Id, agendamento.DataHoraAtendimento, paciente.Cpf, medico.Crm));

            return commit;
        }

        public async Task<ValidationResult> Cancelar(Guid id)
        {

            var agendamento = await _agendamentoRepository.ObterPorId(id);

            if(agendamento == null)
            {
                AdicionarErro("Agendamento não localizado!");
                return ValidationResult;
            }

            if (agendamento.StatusAgendamento == StatusAgendamento.Cancelado)
            {
                AdicionarErro("Esse agendamento já se encontra cancelado");
                return ValidationResult;
            }

            if(DateTime.Now.Date.AddDays(CalcularAntecedenciaDiasUteis()) > agendamento.DataHoraAtendimento.Date)
            {
                AdicionarErro("Agendamento só pode ser cancelado com pelo menos 2 dias de antecedencia (Desconsiderando sábado e domingo)!");
                return ValidationResult;
            }

            agendamento.CancelarAgendamento();

            var commit = await PersistirDados(_agendamentoRepository.UnitOfWork);

            if(commit.IsValid) await _bus.Publish(new AgendamentoCanceladoEvent(agendamento.Id));

            return commit;
        }

        private async Task ValidarAgendamento(Agendamento agendamento)
        {
            var medicoEspecialidade = await _especialidadeMedicoRepository.ObterPorId(agendamento.EspecialidadeMedicoId);
            //Verificar se é sabado ou domingo
            if (agendamento.DataHoraAtendimento.DayOfWeek == DayOfWeek.Saturday || agendamento.DataHoraAtendimento.DayOfWeek == DayOfWeek.Sunday)
            {
                AdicionarErro("Atendimento de segunda a sexta!");
                return;
            }

            //Agendamento deve ocorrer com pelo menos 2 horas de antecedencia
            if (agendamento.DataHoraAtendimento.AddHours(2) <= DateTime.Now)
            {
                AdicionarErro("Agendamento deve ocorrer com pelo 2 horas de antecedencia!");
                return;
            }

            if (agendamento.DataHoraAtendimento.TimeOfDay < new TimeSpan(9, 0, 0) || agendamento.DataHoraAtendimento.TimeOfDay > new TimeSpan(18, 0, 0))
            {
                AdicionarErro("Atendimento das 09:00 até as 18 horas!");
                return;
            }

            if (medicoEspecialidade == null)
            {
                AdicionarErro("EspecialidadeMedico não localizada!");
                return;
            }

            //Verificar se o médico tem agenda livre
            if (!await _especialidadeMedicoRepository.VerificarAgendaLivreMedico(medicoEspecialidade.MedicoId, agendamento.DataHoraAtendimento))
            {
                AdicionarErro("Esse médico não poderá te atender nesse horário!");
                return;
            }

            //Verificar se a hora atendimento é fracionada
            if (!VerificarHoraFracionada(agendamento.DataHoraAtendimento))
            {
                AdicionarErro("A hora de atendimento não pode ser fracionada");
                return;
            }

            //Verificar se o paciente está cadastrado na base;
            if (!await _pacienteRepository.VerificarPacienteCadastrado(agendamento.PacienteId))
            {
                AdicionarErro("Paciente não localizado");
                return;
            }

            if (!await _pacienteRepository.VerificarAgendaLivrePaciente(agendamento.PacienteId, agendamento.DataHoraAtendimento))
            {
                AdicionarErro("Esse paciente não pode ser atendimento nesse horário!");
                return;
            }
            
        }

        private bool VerificarHoraFracionada(DateTime data)
        {
            return data.Minute == 0;
        }
        
        //Essa função calcula a quantidade de dias que deve retornar para desconsiderando fins de semana
        private int CalcularAntecedenciaDiasUteis()
        {
            int antecedenciaDiasUtei = 0;
            int diasUteisCalculados = 0;
            DateTime DataCorrente = DateTime.Now.AddDays(0);
            while (antecedenciaDiasUtei < 2)
            {
                DataCorrente = DataCorrente.AddDays(1);
                if (DataCorrente.DayOfWeek != DayOfWeek.Saturday && DataCorrente.DayOfWeek != DayOfWeek.Sunday)// Se for diferente de sabado e domingo a DiasAntecedencia         
                    antecedenciaDiasUtei++;                            
                diasUteisCalculados++;
            }
            return diasUteisCalculados;
        }

        public async Task<Agendamento> ObterPorId(Guid id)
        {
            var retorno = await _agendamentoRepository.ObterAgendamentoPorId(id);

            if (retorno != null)
                _logger.LogInformation("Obtem agendamento por ID na Service.");

            return retorno;
        }

        public async Task<IEnumerable<Agendamento>> ObterTodos()
        {
            var retorno = await _agendamentoRepository.ObterTodosAgendamentos();
            if (retorno != null)
                _logger.LogInformation("Obtem agendamento por ID na Service.");
            return retorno;
        }
        public async Task<IEnumerable<Agendamento>> ObterAgendamentosPorIStatus(int status)
        {
            var retorno = await _agendamentoRepository.ObterAgendamentosPorIStatus(status);

            if (retorno != null)
                _logger.LogInformation("Obtem agendamento por Status na Service.");

            return retorno;
        }
    }
}

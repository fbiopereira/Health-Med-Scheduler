using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Enums;
using HealthMedScheduler.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace HealthMedScheduler.Application.Features.Agendamentos.Commands.AtualizarAgendamento
{
    public class RealizarReagendamentoCommandHandler : IRequestHandler<RealizarReagendamentoCommand, Guid>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        public RealizarReagendamentoCommandHandler(IAgendamentoRepository agendamentoRepository, IEspecialidadeMedicoRepository especialidadeMedicoRepository, IPacienteRepository pacienteRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _especialidadeMedicoRepository = especialidadeMedicoRepository;
            _pacienteRepository = pacienteRepository;
        }
        public async Task<Guid> Handle(RealizarReagendamentoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new RealizarReagendamentoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Agendamento inválido", validationResult);

            var agendamento = await _agendamentoRepository.ObterPorId(request.Id);

            if (agendamento == null)
            {
                throw new BadRequestException("Agendamento não localizado!", validationResult);
            }

            if (agendamento.StatusAgendamento == StatusAgendamento.Cancelado)
            {
                throw new BadRequestException("Esse agendamento se encontra cancelado!", validationResult);
            }

            if (DateTime.Now.Date.AddDays(CalcularAntecedenciaDiasUteis()) > agendamento.DataHoraAtendimento.Date)
            {
                throw new BadRequestException("Reagendamento deve ser realizado com pelo menos 2 dias de antecedencia (Desconsiderando sábado e domingo)!", validationResult);
            }

            agendamento.AlterarDataAgendamento(request.DataHoraAtendimento);

            await ValidarAgendamento(agendamento);

            await _agendamentoRepository.Atualizar(agendamento);

            await _agendamentoRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return agendamento.Id;
        }

        private async Task ValidarAgendamento(Agendamento agendamento)
        {
            var medicoEspecialidade = await _especialidadeMedicoRepository.ObterPorId(agendamento.EspecialidadeMedicoId);
            //Verificar se é sabado ou domingo
            if (agendamento.DataHoraAtendimento.DayOfWeek == DayOfWeek.Saturday || agendamento.DataHoraAtendimento.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new BadRequestException("Atendimento de segunda a sexta!", new ValidationResult());
            }

            //Agendamento deve ocorrer com pelo menos 2 horas de antecedencia
            if (agendamento.DataHoraAtendimento.AddHours(2) <= DateTime.Now)
            {
                throw new BadRequestException("Agendamento deve ocorrer com pelo 2 horas de antecedencia!", new ValidationResult());
            }

            if (agendamento.DataHoraAtendimento.TimeOfDay < new TimeSpan(9, 0, 0) || agendamento.DataHoraAtendimento.TimeOfDay > new TimeSpan(18, 0, 0))
            {
                throw new BadRequestException("Atendimento das 09:00 até as 18 horas!", new ValidationResult());
            }

            if (medicoEspecialidade == null)
            {
                throw new BadRequestException("EspecialidadeMedico não localizada!", new ValidationResult());
            }

            //Verificar se o médico tem agenda livre
            if (!await _especialidadeMedicoRepository.VerificarAgendaLivreMedico(medicoEspecialidade.MedicoId, agendamento.DataHoraAtendimento))
            {
                throw new BadRequestException("Esse médico não poderá te atender nesse horário!", new ValidationResult());
            }

            //Verificar se a hora atendimento é fracionada
            if (!VerificarHoraFracionada(agendamento.DataHoraAtendimento))
            {
                throw new BadRequestException("A hora de atendimento não pode ser fracionada", new ValidationResult());
            }

            //Verificar se o paciente está cadastrado na base;
            if (!await _pacienteRepository.VerificarPacienteCadastrado(agendamento.PacienteId))
            {
                throw new BadRequestException("Paciente não localizado", new ValidationResult());
            }

            if (!await _pacienteRepository.VerificarAgendaLivrePaciente(agendamento.PacienteId, agendamento.DataHoraAtendimento))
            {
                throw new BadRequestException("Esse paciente não pode ser atendimento nesse horário!", new ValidationResult());
            }
        }

        private bool VerificarHoraFracionada(DateTime data)
        {
            return data.Minute == 0;
        }

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
    }
}

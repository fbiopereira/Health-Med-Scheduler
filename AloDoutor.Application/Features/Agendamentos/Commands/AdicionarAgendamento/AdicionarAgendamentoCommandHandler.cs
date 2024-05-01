using AloDoutor.Application.Exceptions;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using FluentValidation.Results;
using MediatR;

namespace AloDoutor.Application.Features.Agendamentos.Commands.AdicionarAgendamento
{
    public class AdicionarAgendamentoCommandHandler : IRequestHandler<AdicionarAgendamentoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        public AdicionarAgendamentoCommandHandler(IMapper mapper, IAgendamentoRepository agendamentoRepository, IEspecialidadeMedicoRepository especialidadeMedicoRepository, IPacienteRepository pacienteRepository)
        {
            _mapper = mapper;
            _agendamentoRepository = agendamentoRepository;
            _especialidadeMedicoRepository = especialidadeMedicoRepository;
            _pacienteRepository = pacienteRepository;
        }
        public async Task<Guid> Handle(AdicionarAgendamentoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AdicionarAgendamentoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Agendamento inválido", validationResult);

            var agendamento = _mapper.Map<Agendamento>(request);

            await ValidarAgendamento(agendamento);

            await _agendamentoRepository.Adicionar(agendamento);

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
    }
}

using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using HealthMedScheduler.Application.Interfaces.Email;
using System.Net.Mail;
using HealthMedScheduler.Application.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HealthMedScheduler.Application.Features.Agendamentos.Commands.AdicionarAgendamento
{
    public class AdicionarAgendamentoCommandHandler : IRequestHandler<AdicionarAgendamentoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAgendaMedicoRepository _agendaMedicoRepository;

        public AdicionarAgendamentoCommandHandler(IMapper mapper, IAgendamentoRepository agendamentoRepository, IEspecialidadeMedicoRepository especialidadeMedicoRepository, IPacienteRepository pacienteRepository, IEmailSender emailSender, IMedicoRepository medicoRepository, IAgendaMedicoRepository agendaMedicoRepository)
        {
            _mapper = mapper;
            _agendamentoRepository = agendamentoRepository;
            _especialidadeMedicoRepository = especialidadeMedicoRepository;
            _pacienteRepository = pacienteRepository;
            _emailSender = emailSender;
            _medicoRepository = medicoRepository;
            _agendaMedicoRepository = agendaMedicoRepository;
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

            var especialidadeMedico = await _especialidadeMedicoRepository.ObterPorId(request.EspecialidadeMedicoId);

            var medico = await _medicoRepository.ObterPorId(especialidadeMedico.MedicoId);

            var paciente = await _pacienteRepository.ObterPorId(agendamento.PacienteId);
            try
            {
                var email = new EmailViewModel
                {
                    ToEmail = $"{medico.Email}",
                    Body = $"Olá, Dr. {medico.Nome}! Você tem uma nova consulta marcada! Paciente: {paciente.Nome}. Data e horário: {request.DataHoraAtendimento.ToString("dd/MM/yyyy")} às {request.DataHoraAtendimento.ToString("HH:mm")}",
                    Subject = "Health&Med - Nova consulta agendada"
                };
                await _emailSender.SendEmailAsync(email);
            }          
            catch (Exception)
            {
                throw new BadRequestException("Não foi possível enviar o email.", new ValidationResult());
            }
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

            //Verificar se a hora atendimento é fracionada
            if (!VerificarHoraFracionada(agendamento.DataHoraAtendimento))
            {
                throw new BadRequestException("A hora de atendimento não pode ser fracionada", new ValidationResult());
            }

            if (medicoEspecialidade == null)
            {
                throw new BadRequestException("EspecialidadeMedico não localizada!", new ValidationResult());
            }

            //Validar se o médico tem agenda nesse dia é horario
            var diaSemana = (int) agendamento.DataHoraAtendimento.DayOfWeek;
            var agendaMedico = await _agendaMedicoRepository.ObterAgendaMedicoPorDia(medicoEspecialidade.MedicoId, diaSemana);
            if(agendaMedico == null)
            {
                throw new BadRequestException("Esse médico não tem agenda para esse dia!", new ValidationResult());
            }

            //Caso o medico tenha agenda, precisa verificar se ele pode realizar o atendimento nesse horário
            if(agendamento.DataHoraAtendimento.TimeOfDay < agendaMedico.HoraInicio || agendamento.DataHoraAtendimento.TimeOfDay > agendaMedico.HoraFim)
            {
                throw new BadRequestException("Esse médico não pode realizar o atendimento nesse horário!", new ValidationResult());
            }

            //Verificar se o médico tem agenda marcada nesse horario
            var boolEspecialidade = await _especialidadeMedicoRepository.VerificarAgendaLivreMedico(medicoEspecialidade.MedicoId, agendamento.DataHoraAtendimento);
            if (!boolEspecialidade)
            {
                throw new BadRequestException("Esse médico não poderá te atender nesse horário!", new ValidationResult());
            }            

            //Verificar se o paciente está cadastrado na base;
            if (!await _pacienteRepository.VerificarPacienteCadastrado(agendamento.PacienteId))
            {
                throw new BadRequestException("Paciente não localizado", new ValidationResult());
            }

            if (!await _pacienteRepository.VerificarAgendaLivrePaciente(agendamento.PacienteId, agendamento.DataHoraAtendimento))
            {
                throw new BadRequestException("Esse paciente não pode ser atendimento nesse horário!", new ValidationResult());
            };
        }

        private bool VerificarHoraFracionada(DateTime data)
        {
            return data.Minute == 0;
        }
    }
}

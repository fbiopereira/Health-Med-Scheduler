using AutoMapper;
using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using MediatR;

namespace HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarAgenda
{
    public class AdicionarAgendaMedicoCommandHandler : IRequestHandler<AdicionarAgendaMedicoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAgendaMedicoRepository _agendaMedicoRepository;
        private readonly IMedicoRepository _medicoRepository;

        public AdicionarAgendaMedicoCommandHandler(IMapper mapper, IAgendaMedicoRepository agendaMedicoRepository, IMedicoRepository medicoRepository)
        {
            _mapper = mapper;
            _agendaMedicoRepository = agendaMedicoRepository;
            _medicoRepository = medicoRepository;
        }

        public async Task<Guid> Handle(AdicionarAgendaMedicoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AdicionarAgendaMedicoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Médico inválido", validationResult);

            var agendaMedico =  _mapper.Map<AgendaMedico>(request);
            

            var medicoExistente = await _medicoRepository.ObterPorId(request.MedicoId);
            if (medicoExistente == null)
                throw new BadRequestException("Médico não cadastrado", validationResult);

            var agendaExistente = await _agendaMedicoRepository.ObterAgendaMedicoPorDia(request.MedicoId, request.DiaSemana);

            if (agendaExistente == null)
            {
                await _agendaMedicoRepository.Adicionar(agendaMedico);
            }
            else
            {
                agendaExistente.AtualizarHoraInicio(TimeSpan.Parse(request.HoraInicio));
                agendaExistente.AtualizarHoraFim(TimeSpan.Parse(request.HoraFim));
                await _agendaMedicoRepository.Atualizar(agendaExistente);
            }

            await _agendaMedicoRepository.UnitOfWork.Commit();

            return agendaMedico.Id;
        }
    }
}

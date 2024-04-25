using AloDoutor.Application.Exceptions;
using AloDoutor.Application.Interfaces;
using AloDoutor.Domain.Entity;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AloDoutor.Application.Features.Medicos.Commands.AtualizarMedico
{
    public class AtualizarMedicoCommandHandler : IRequestHandler<AtualizarMedicoCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IMedicoRepository _medicoRepository;
        private readonly ILogger<AtualizarMedicoCommandHandler> _logger;

        public AtualizarMedicoCommandHandler(IMapper mapper, IMedicoRepository medicoRepository, ILogger<AtualizarMedicoCommandHandler> logger)
        {
            _mapper = mapper;
            _medicoRepository = medicoRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(AtualizarMedicoCommand request, CancellationToken cancellationToken)
        {
            // Validar dados recebidos
            var validator = new AtualizarMedicoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Erros de validação na solicitação de atualização para {0} - {1}", nameof(Medico), request.Id);
                throw new BadRequestException("Tipo de Médico Inválido", validationResult);
            }

            // Converter em objeto de entidade de domínio
            var medicoParaAtualizar = _mapper.Map<Medico>(request);

            // Adicionar ao banco de dados
            await _medicoRepository.Atualizar(medicoParaAtualizar);
            await _medicoRepository.UnitOfWork.Commit();
            // Retornar Unit para indicar sucesso
            return Unit.Value;
        }
    }
    
}

using AloDoutor.Application.Exceptions;
using AloDoutor.Application.Interfaces;
using AloDoutor.Domain.Entity;
using AutoMapper;
using MediatR;

namespace AloDoutor.Application.Features.Medicos.Commands.AtualizarMedico
{
    public class AtualizarMedicoCommandHandler : IRequestHandler<AtualizarMedicoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IMedicoRepository _medicoRepository;
        public AtualizarMedicoCommandHandler(IMapper mapper, IMedicoRepository medicoRepository)
        {
            _mapper = mapper;
            _medicoRepository = medicoRepository;
        }

        public async Task<Guid> Handle(AtualizarMedicoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AtualizarMedicoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Médico inválido", validationResult);

            if (!_medicoRepository.Buscar(p => p.Id == request.Id).Result.Any())
            {
                throw new BadRequestException("Medico Não localizado!", validationResult);

            }

            //Validar se existe algum cpf ou crm com esse mesmo numero vinculado a algum outro medico
            if (_medicoRepository.Buscar(p => (p.Cpf == request.Cpf || p.Crm == request.Cpf) && p.Id != request.Id).Result.Any())
            {
                throw new BadRequestException("Falha ao atualizar Médico!", validationResult);
            }

            //Converter para objeto entidade no dominio
            var medicoAtualizado = _mapper.Map<Medico>(request);

            await _medicoRepository.Atualizar(medicoAtualizado);
            await _medicoRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return medicoAtualizado.Id;
        }
    }
}

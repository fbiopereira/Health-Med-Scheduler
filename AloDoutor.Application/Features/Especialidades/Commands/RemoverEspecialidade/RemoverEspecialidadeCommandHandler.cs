using AloDoutor.Application.Exceptions;
using AloDoutor.Domain.Interfaces;
using MediatR;

namespace AloDoutor.Application.Features.Especialidades.Commands.RemoverEspecialidade
{
    public class RemoverEspecialidadeCommandHandler : IRequestHandler<RemoverEspecialidadeCommand, Guid>
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;
        public RemoverEspecialidadeCommandHandler(IEspecialidadeRepository especialdiadeRepository)
        {
            _especialidadeRepository = especialdiadeRepository;
        }

        public async Task<Guid> Handle(RemoverEspecialidadeCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new RemoverEspecialidadeCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Especialidade inválido", validationResult);

            if (!_especialidadeRepository.Buscar(p => p.Id == request.idEspecialidade).Result.Any())
            {
                throw new BadRequestException("Especialidade Não localizado!", validationResult);
            }

            await _especialidadeRepository.Remover(await _especialidadeRepository.ObterPorId(request.idEspecialidade));

            await _especialidadeRepository.UnitOfWork.Commit();

            return request.idEspecialidade;
        }
    }
}

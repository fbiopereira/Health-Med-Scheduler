using AloDoutor.Application.Exceptions;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using MediatR;

namespace AloDoutor.Application.Features.Medicos.Commands.DeletarMedico
{
    public class DeletarMedicoCommandHandler : IRequestHandler<DeletarMedicoCommand, Unit>
    {

        private readonly IMedicoRepository _medicoRepository;

        public DeletarMedicoCommandHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }
        public async Task<Unit> Handle(DeletarMedicoCommand request, CancellationToken cancellationToken)
        {
            // Recuperar objeto de entidade de domínio
            var medicoParaDeletar = await _medicoRepository.ObterPorId(request.Id);

            // Verificar se o registro existe
            if (medicoParaDeletar == null)
                throw new NotFoundException(nameof(Medico), request.Id);

            // Remover do banco de dados
            await _medicoRepository.Remover(medicoParaDeletar);
            await _medicoRepository.UnitOfWork.Commit();

            // Retorna Unit para indicar sucesso
            return Unit.Value;
        }
    }
}

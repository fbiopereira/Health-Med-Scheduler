using AloDoutor.Application.Exceptions;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AloDoutor.Application.Features.Medicos.Commands.AdicionarMedico
{
    public class AdicionarMedicoCommandHandler : IRequestHandler<AdicionarMedicoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IMedicoRepository _medicoRepository;
        public AdicionarMedicoCommandHandler(IMapper mapper, IMedicoRepository medicoRepository)
        {
            _mapper = mapper;
            _medicoRepository = medicoRepository;
        }
        public async Task<Guid> Handle(AdicionarMedicoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AdicionarMedicoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Médico inválido", validationResult);

            var medicoCPFExistente = await _medicoRepository.ObterMedicoPorCPF(request.Cpf);
            if(medicoCPFExistente != null)
            {
                throw new BadRequestException("Esse CPF Já se encontra cadastrado!", validationResult);
            }

            var medicoCRMExistente = await _medicoRepository.ObterMedicoPorCRM(request.Crm);
            if (medicoCRMExistente != null)
            {
                throw new BadRequestException("Esse CRF Já se encontra cadastrado!", validationResult);
            }

            //Converter para objeto entidade no dominio
            var medicoCriado = _mapper.Map<Medico>(request);

            // Adicionar a base de dados
            await _medicoRepository.Adicionar(medicoCriado);
            await _medicoRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return medicoCriado.Id;
        }
    }
}

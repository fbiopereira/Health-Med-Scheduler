using AloDoutor.Application.Exceptions;
using AloDoutor.Application.Interfaces;
using AloDoutor.Domain.Entity;
using AutoMapper;
using MediatR;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialdiadeMedico
{
    public class AdicionarEspecialidadeMedicoCommandHandler : IRequestHandler<AdicionarEspecialidadeMedicoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        private readonly IEspecialidadeRepository _especialidadeRepository;
        private readonly IMedicoRepository _medicoRepository;
        public AdicionarEspecialidadeMedicoCommandHandler(IMapper mapper, IEspecialidadeMedicoRepository especialdidadeMedicoRepository, IEspecialidadeRepository especialidadeRepository, IMedicoRepository medicoRepository)
        {
            _mapper = mapper;
            _especialidadeMedicoRepository = especialdidadeMedicoRepository;
            _especialidadeRepository = especialidadeRepository;
            _medicoRepository = medicoRepository;
        }
        public async Task<Guid> Handle(AdicionarEspecialidadeMedicoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AdicionarEspecialidadeMedicoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Especialidade Médico inválido", validationResult);

            //Verificar se a especialidade está cadastrada na base de dados
            if (!_especialidadeRepository.Buscar(e => e.Id == request.EspecialidadeId).Result.Any())
            {
                throw new BadRequestException("Especialidade não cadastrada na base de dados! ", validationResult);
            }

            //Verificar se o medico está cadastrado na base de dados
            if (!_medicoRepository.Buscar(m => m.Id == request.MedicoId).Result.Any())
            {
                throw new BadRequestException("Medico não cadastrado na base de dados! ", validationResult);
            }

            //Verificar se já existe o medico já está vinculado a essa especialidade
            if (_especialidadeMedicoRepository.Buscar(e => e.EspecialidadeId == request.EspecialidadeId && e.MedicoId == request.MedicoId).Result.Any())
            {
                throw new BadRequestException("Esse médico já está vinculado a essa especialidade! ", validationResult);
            }

            //Converter para objeto entidade no dominio
            var especialiadeMedicoCriado = _mapper.Map<EspecialidadeMedico>(request);

            // Adicionar a base de dados
            await _especialidadeMedicoRepository.Adicionar(especialiadeMedicoCriado);
            await _especialidadeMedicoRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return especialiadeMedicoCriado.Id;
        }
    }
}

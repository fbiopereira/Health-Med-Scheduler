using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarMedico
{
    public class AdicionarMedicoCommandHandler : IRequestHandler<AdicionarMedicoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IMedicoRepository _medicoRepository; 
        private readonly UserManager<IdentityUser> _userManager;
        public AdicionarMedicoCommandHandler(IMapper mapper, IMedicoRepository medicoRepository, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _medicoRepository = medicoRepository;
            _userManager = userManager;
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

            //Cadastrar login medico
            if (!await CriarAcessoMedico(request))
            {
                throw new BadRequestException("Falha ao cadastrar o acesso do médico!", validationResult);
            }

            // Adicionar a base de dados
            await _medicoRepository.Adicionar(medicoCriado);
            await _medicoRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return medicoCriado.Id;
        }

        private async Task<bool> CriarAcessoMedico(AdicionarMedicoCommand request)
        {
            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            return result.Succeeded;
        }
    }
}

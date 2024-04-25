using FluentValidation;

namespace AloDoutor.Application.Features.Medicos.Commands.AtualizarMedico
{
    public class AtualizarMedicoCommandValidator : AbstractValidator<AtualizarMedicoCommand>
    {
        public AtualizarMedicoCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Nome).NotEmpty();
            RuleFor(x => x.Cpf).NotEmpty();
            RuleFor(x => x.Cep).NotEmpty();
            RuleFor(x => x.Endereco).NotEmpty();
            RuleFor(x => x.Estado).NotEmpty();
            RuleFor(x => x.Crm).NotEmpty();
            RuleFor(x => x.Telefone).NotEmpty();
        }
    }
}

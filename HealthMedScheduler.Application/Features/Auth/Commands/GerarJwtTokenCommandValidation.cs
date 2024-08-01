using FluentValidation;

namespace HealthMedScheduler.Application.Features.Auth.Commands
{
    public class GerarJwtTokenCommandValidation : AbstractValidator<GerarJwtTokenCommand>
    {
        public GerarJwtTokenCommandValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.Senha)
                    .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}

using MediatR;

namespace AloDoutor.Application.Features.Pacientes.Commands.RemoverPaciente
{
    public class RemoverPacienteCommand : IRequest<Guid>
    {
        public Guid IdPaciente { get; set; }

    }
}

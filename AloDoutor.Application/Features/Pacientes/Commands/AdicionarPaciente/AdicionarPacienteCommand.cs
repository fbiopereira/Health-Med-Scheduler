using MediatR;

namespace AloDoutor.Application.Features.Pacientes.Commands.AdicionarPaciente
{
    public class AdicionarPacienteCommand : IRequest<Guid>
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Idade { get; set; }
        public string Telefone { get; set; }
    }
}

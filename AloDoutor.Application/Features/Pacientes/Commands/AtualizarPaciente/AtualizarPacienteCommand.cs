using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AloDoutor.Application.Features.Pacientes.Commands.AtualizarPaciente
{
    public class AtualizarPacienteCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Idade { get; set; }
        public string Telefone { get; set; }

    }
}

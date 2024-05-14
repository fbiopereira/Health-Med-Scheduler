using MediatR;

namespace AloDoutor.Application.Features.Pacientes.Commands.AtualizarPaciente
{
    public class AtualizarPacienteCommand : IRequest<Guid>
    {
        public AtualizarPacienteCommand(Guid id, string nome, string cpf, string cep, string endereco, string estado, string idade, string telefone)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Cep = cep;
            Endereco = endereco;
            Estado = estado;
            Idade = idade;
            Telefone = telefone;
        }

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

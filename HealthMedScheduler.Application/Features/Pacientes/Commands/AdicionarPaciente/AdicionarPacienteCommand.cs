using MediatR;

namespace HealthMedScheduler.Application.Features.Pacientes.Commands.AdicionarPaciente
{
    public class AdicionarPacienteCommand : IRequest<Guid>
    {

        public AdicionarPacienteCommand(string nome, string cpf, string cep, string endereco, string estado, string idade, string telefone, string email, string password, string confirmPassword)
        {
            Nome = nome;
            Cpf = cpf;
            Cep = cep;
            Endereco = endereco;
            Estado = estado;
            Idade = idade;
            Telefone = telefone;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
        public AdicionarPacienteCommand() { }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Idade { get; set; }
        public string Telefone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}

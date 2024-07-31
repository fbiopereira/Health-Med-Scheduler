using MediatR;

namespace HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarMedico
{
    public class AdicionarMedicoCommand : IRequest<Guid>
    {
        public AdicionarMedicoCommand() { }
        public AdicionarMedicoCommand(string? nome, string? cpf, string? cep, string? endereco, string? estado, string? crm, string? telefone, string email, string password, string confirmPassword)
        {
            Nome = nome;
            Cpf = cpf;
            Cep = cep;
            Endereco = endereco;
            Estado = estado;
            Crm = crm;
            Telefone = telefone;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Cep { get; set; }
        public string? Endereco { get; set; }
        public string? Estado { get; set; }
        public string? Crm { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}

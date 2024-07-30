using MediatR;

namespace HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarMedico
{
    public class AdicionarMedicoCommand : IRequest<Guid>
    {
        public AdicionarMedicoCommand() { }
        public AdicionarMedicoCommand(string? nome, string? cpf, string? cep, string? endereco, string? estado, string? crm, string? telefone)
        {
            Nome = nome;
            Cpf = cpf;
            Cep = cep;
            Endereco = endereco;
            Estado = estado;
            Crm = crm;
            Telefone = telefone;
        }

        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Cep { get; set; }
        public string? Endereco { get; set; }
        public string? Estado { get; set; }
        public string? Crm { get; set; }
        public string? Telefone { get; set; }
    }
}

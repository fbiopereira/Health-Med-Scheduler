using MediatR;

namespace AloDoutor.Application.Features.Medicos.Commands.AtualizarMedico
{
    public class AtualizarMedicoCommand : IRequest<Guid>
    {
        public AtualizarMedicoCommand(Guid id, string? nome, string? cpf, string? cep, string? endereco, string? estado, string? crm, string? telefone)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Cep = cep;
            Endereco = endereco;
            Estado = estado;
            Crm = crm;
            Telefone = telefone;
        }

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Cep { get; set; }
        public string? Endereco { get; set; }
        public string? Estado { get; set; }
        public string? Crm { get; set; }
        public string? Telefone { get; set; }

    }
}

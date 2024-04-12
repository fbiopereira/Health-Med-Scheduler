using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AloDoutor.Application.Features.Medicos.Commands.AdicionarMedico
{
    public class AdicionarMedicoCommand : IRequest<Guid>
    {
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Cep { get; set; }
        public string? Endereco { get; set; }
        public string? Estado { get; set; }
        public string? Crm { get; set; }
        public string? Telefone { get; set; }
    }
}

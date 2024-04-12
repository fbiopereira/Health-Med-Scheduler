using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AloDoutor.Application.Features.Medicos.Queries.ObterTodosMedicos
{
    public record MedicoDTO(string Nome, string Cpf, string Cep, string Endereco, string Estado, string Crm, string Telefone);
    
}

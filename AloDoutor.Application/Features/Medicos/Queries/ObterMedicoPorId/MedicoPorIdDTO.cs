namespace AloDoutor.Application.Features.Medicos.Queries.ObterMedicoPorId
{
    public record MedicoPorIdDTO(Guid Id, string Nome, string Cpf, string Cep, string Endereco, string Estado, string Crm, string Telefone);
  
}

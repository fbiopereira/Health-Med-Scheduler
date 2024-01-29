using System.Text.Json.Serialization;

namespace AloDoutor.Api.Application.ViewModel
{
    public class MedicoViewModel
    {
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Cep { get; private set; }
        public string Endereco { get; private set; }
        public string Estado { get; private set; }
        public string Crm { get; private set; }
        public string Telefone { get; private set; }
        public IEnumerable<MedicoEspecialidadesViewModel>? Especialidades { get; private set; }
        public IEnumerable<AgendamentoMedicoViewModel>? agendasMedico { get; private set; }
    }
}

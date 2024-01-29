using System.Text.Json.Serialization;

namespace AloDoutor.Api.Application.ViewModel
{
    public class PacienteViewModel
    {
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Cep { get; private set; }
        public string Endereco { get; private set; }
        public string Estado { get; private set; }
        public string idade { get; private set; }
        public string Telefone { get; private set; }
        public IEnumerable<AgendamentoPacienteViewModel> agendasPaciente { get; private set; }
    }
}

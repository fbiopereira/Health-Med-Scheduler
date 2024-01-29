using System.Text.Json.Serialization;

namespace AloDoutor.Api.Application.ViewModel
{
    public class EspecialidadeMedicosViewModel
    {
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Crm { get; private set; }
        public string Telefone { get; private set; }
    }
}

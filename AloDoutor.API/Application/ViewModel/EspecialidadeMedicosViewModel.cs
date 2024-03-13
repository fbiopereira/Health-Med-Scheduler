using System.Text.Json.Serialization;

namespace AloDoutor.Api.Application.ViewModel
{
    public class EspecialidadeMedicosViewModel
    {
        public string Id { get; private set; }
        public Guid EspecialidadeId { get; private set; }
        public Guid MedicoId { get; private set; }
        public DateTime DataRegistro { get; private set; }
    }
}
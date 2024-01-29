using System.Text.Json.Serialization;

namespace AloDoutor.Api.Application.ViewModel
{
    public class EspecialidadeViewModel
    {
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public IEnumerable<EspecialidadeMedicosViewModel> Medicos { get; private set; }

    }
}

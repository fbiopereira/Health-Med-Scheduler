namespace AloDoutor.Api.Application.ViewModel
{
    public class AgendamentoMedicoViewModel
    {
        public string Id { get; private set; }
        public string NomePaciente { get; private set; }
        public string NomeEspecialidade { get; private set; }
        public DateTime DataHoraAtendimento { get; private set; }
        public string StatusAgendamento { get; private set; }
    }
}

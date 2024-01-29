namespace AloDoutor.Api.Application.ViewModel
{
    public class AgendamentoViewModel
    {
        public string Id { get; private set; }
        public string NomePaciente { get; private set; }

        public string CpfPaciente { get; private set; }
        public string NomeMedico { get; private set; }
        public string CrmMedico { get; private set; }
        public string NomeEspecialidade { get; private set; }
        public DateTime DataHoraAtendimento { get; private set; }
        public string StatusAgendamento { get; private set; }
    }
}

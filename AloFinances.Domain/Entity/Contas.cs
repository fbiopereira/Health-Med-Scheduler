using AloDoutor.Core.DomainObjects;

namespace AloFinances.Domain.Entity
{
    public class Contas : Entidade
    {
        public Contas() { }

        public Contas(Guid pacienteId, Guid medicoId, DateTime dataVencimento, StatusConta statusConta, Guid agendamentoId)
        {
            PacienteId = pacienteId;
            MedicoId = medicoId;
            DataCadastro = DateTime.Now;
            DataVencimento = dataVencimento;
            StatusConta = statusConta;
            AgendamentoId = agendamentoId;
        }

        public int Codigo { get; private set; }
        public Guid AgendamentoId { get; private set; }
        public Guid PacienteId { get; private set; }
        public Guid MedicoId { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public StatusConta StatusConta { get; private set; }

        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }

        public void AtualizarConta(Guid pacienteId, Guid medicoId, DateTime dataVencimento)
        {
            PacienteId = pacienteId;
            MedicoId = medicoId;
            DataVencimento = dataVencimento;
        }

        public void CalcularValor(decimal valorMedico, decimal valorDefault)
        {
            Valor = valorMedico != 0 ? valorMedico : valorDefault;
        }

        public void CancelarConta()
        {
            StatusConta = StatusConta.CANCELADA;
        }
    }
}

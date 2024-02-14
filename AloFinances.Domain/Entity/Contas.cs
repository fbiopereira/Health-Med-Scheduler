using AloDoutor.Core.DomainObjects;

namespace AloFinances.Domain.Entity
{
    public class Contas : Entidade
    {
        public Contas() { }

        public Contas(int codigo, Guid pacienteId, Guid medicoId, decimal valor, DateTime dataCadastro, DateTime dataVencimento, StatusConta statusConta, Guid agendamentoId)
        {
            Codigo = codigo;
            PacienteId = pacienteId;
            MedicoId = medicoId;
            Valor = valor;
            DataCadastro = dataCadastro;
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
    }
}

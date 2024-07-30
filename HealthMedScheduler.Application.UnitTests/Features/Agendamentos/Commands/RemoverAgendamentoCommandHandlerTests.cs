using HealthMedScheduler.Application.Features.Agendamentos.Commands.AtualizarAgendamento;
using HealthMedScheduler.Application.Features.Agendamentos.Commands.RemoverAgendamento;
using HealthMedScheduler.Application.Features.Medicos.Commands.RemoverMedico;
using HealthMedScheduler.Application.UnitTests.Mocks;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.Agendamentos.Commands
{
    [Collection(nameof(PacienteTestsAutoMockerCollection))]
    public class RemoverAgendamentoCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAgendamentoRepository> _mockAgendamentoRepo;
        private readonly RemoverAgendamentoCommandHandler _agendamentoHandler;
        private readonly AutoMocker _mocker;
        private readonly PacienteTestsAutoMockerFixture _pacientefixture;

        public RemoverAgendamentoCommandHandlerTests(PacienteTestsAutoMockerFixture pacientefixture)
        {
            _mocker = new AutoMocker();
            _agendamentoHandler = _mocker.CreateInstance<RemoverAgendamentoCommandHandler>();
            _pacientefixture = pacientefixture;
        }

        [Fact(DisplayName = "Excluir agenamento com Sucesso")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task ExcluirAgendamento_MedicoAgendamento_DeveExecutarComSucesso()
        {
            var especialidadeMedico = new EspecialidadeMedico(Guid.NewGuid(), Guid.NewGuid(), DateTime.Parse("2023-05-09 09:00:00"));

            var paciente = _pacientefixture.GerarPacienteValido();
            //Arrange
            var agendamento = new Agendamento(especialidadeMedico.Id, paciente.Id, DateTime.Parse("2024-05-09 09:00:00"));

            var agendamentoCommand = new RemoverAgendamentoCommand
            {
                IdAgendamento = agendamento.Id,               
            };
            var validator = new RemoverAgendamentoCommandValidator();

            //Agendamento
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.Adicionar(It.IsAny<Agendamento>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.ObterPorId(agendamento.Id)).ReturnsAsync(agendamento);
           
            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);
            var result = await _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None);

            //Assert
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Remover(It.IsAny<Agendamento>()), Times.Once);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}

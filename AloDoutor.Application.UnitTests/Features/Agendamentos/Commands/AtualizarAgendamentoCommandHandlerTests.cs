using AloDoutor.Application.Features.Agendamentos.Commands.AdicionarAgendamento;
using AloDoutor.Application.UnitTests.Mocks;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using Moq.AutoMock;
using Moq;
using AloDoutor.Application.MappingProfiles;
using AloDoutor.Domain.Entity;
using AloDoutor.Application.Features.Agendamentos.Commands.AtualizarAgendamento;

namespace AloDoutor.Application.UnitTests.Features.Agendamentos.Commands
{
    [Collection(nameof(PacienteTestsAutoMockerCollection))]
    public class AtualizarAgendamentoCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAgendamentoRepository> _mockAgendamentoRepo;
        private readonly RealizarReagendamentoCommandHandler _agendamentoHandler;
        private readonly AutoMocker _mocker;
        private readonly PacienteTestsAutoMockerFixture _pacientefixture;

        public AtualizarAgendamentoCommandHandlerTests(PacienteTestsAutoMockerFixture pacientefixture)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AgendamentoProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(mapperConfig.CreateMapper());
            _agendamentoHandler = _mocker.CreateInstance<RealizarReagendamentoCommandHandler>();
            _pacientefixture = pacientefixture;
        }

        [Fact(DisplayName = "Atualizar agendamento com Sucesso")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task RealizarReagendamento_NovoAgendamento_DeveExecutarComSucesso()
        {
            var especialidadeMedico = new EspecialidadeMedico(Guid.NewGuid(), Guid.NewGuid(), DateTime.Parse("2023-05-09 09:00:00"));

            var paciente = _pacientefixture.GerarPacienteValido();
            //Arrange
            var agendamento = new Agendamento(especialidadeMedico.Id, paciente.Id, DateTime.Parse("2024-05-09 09:00:00"));

            var agendamentoCommand = new RealizarReagendamentoCommand
            {
                Id = agendamento.Id,
                DataHoraAtendimento = DateTime.Parse("2024-05-09 09:00:00"),
                EspecialidadeMedicoId = especialidadeMedico.Id,
                PacienteId = paciente.Id,
            };
            var validator = new RealizarReagendamentoCommandValidator();

            //Agendamento
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.Adicionar(It.IsAny<Agendamento>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.ObterPorId(agendamento.Id)).ReturnsAsync(agendamento);

            //EspecialidadeMedico
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<EspecialidadeMedico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.ObterPorId(especialidadeMedico.Id)).ReturnsAsync(especialidadeMedico);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.VerificarAgendaLivreMedico(especialidadeMedico.MedicoId, agendamentoCommand.DataHoraAtendimento)).ReturnsAsync(true);

            //Paciente
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.Adicionar(It.IsAny<Paciente>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.ObterPorId(paciente.Id)).ReturnsAsync(paciente);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.VerificarPacienteCadastrado(paciente.Id)).ReturnsAsync(true);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.VerificarAgendaLivrePaciente(paciente.Id, agendamentoCommand.DataHoraAtendimento)).ReturnsAsync(true);

            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);
            var result = await _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None);

            //Assert
            Assert.True(validationResult.IsValid);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Atualizar(It.IsAny<Agendamento>()), Times.Once);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}

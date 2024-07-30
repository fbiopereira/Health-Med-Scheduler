using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Application.Features.Pacientes.Commands.AdicionarPaciente;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Application.UnitTests.Mocks;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.Pacientes.Commands
{
    [Collection(nameof(PacienteTestsAutoMockerCollection))]
    public class AdicionarPacienteCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPacienteRepository> _mockPacienteRepo;
        private readonly AdicionarPacienteCommandHandler _pacienteHandler;
        private readonly PacienteTestsAutoMockerFixture _pacientefixture;
        private readonly AutoMocker _mocker;

        public AdicionarPacienteCommandHandlerTests(PacienteTestsAutoMockerFixture pacientefixture)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<PacienteProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(mapperConfig.CreateMapper());
            _pacienteHandler = _mocker.CreateInstance<AdicionarPacienteCommandHandler>();
            _pacientefixture = pacientefixture;
        }

        [Fact(DisplayName = "Adicionar paciente Novo Paciente com Sucesso")]
        [Trait("Categoria", "Paciente - Paciente Command Handler")]
        public async Task AdicionarPaciente_NovoPaciente_DeveExecutarComSucesso()
        {
            //Arrange
            var pacienteCommand = _pacientefixture.GerarPacienteCommandValido();
            var validator = new AdicionarPacienteCommandValidator();
            _mocker.GetMock<IPacienteRepository>().Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _pacienteHandler.Handle(pacienteCommand, CancellationToken.None);
            var validationResult = await validator.ValidateAsync(pacienteCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            _mocker.GetMock<IPacienteRepository>().Verify(r => r.Adicionar(It.IsAny<Paciente>()), Times.Once);
            _mocker.GetMock<IPacienteRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar novo paciente com Fallha")]
        [Trait("Categoria", "Paciente - Paciente Command Handler")]
        public async Task AdicionarPaciente_CPFPacienteExistente_DeveExecutarComFalha()
        {
            // Arrange
            var pacienteOriginal = _pacientefixture.GerarPacienteValido();
            var pacienteNovo = _pacientefixture.GerarPacienteCommandValido();
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.Adicionar(It.IsAny<Paciente>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.ObterPacientePorCPF(pacienteOriginal.Cpf)).ReturnsAsync(pacienteOriginal);

            //Act
            pacienteNovo.Cpf = pacienteOriginal.Cpf;

            //Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _pacienteHandler.Handle(pacienteNovo, CancellationToken.None));
            _mocker.GetMock<IPacienteRepository>().Verify(r => r.Adicionar(It.IsAny<Paciente>()), Times.Never);
            _mocker.GetMock<IPacienteRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);

        }

        [Fact(DisplayName = "Adicionar paciente Novo Paciente com Falha")]
        [Trait("Categoria", "Paciente - Paciente Command Handler")]
        public async Task AdicionarPaciente_NovoPaciente_DeveExecutarComFalha()
        {
            //Arrange
            var pacienteCommand = _pacientefixture.GerarPacienteCommandInValido();
            var validator = new AdicionarPacienteCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(pacienteCommand);

            //Acert
            Assert.False(validationResult.IsValid);
        }
    }
}

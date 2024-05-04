using AloDoutor.Application.Features.Pacientes.Commands.AtualizarPaciente;
using AloDoutor.Application.MappingProfiles;
using AloDoutor.Application.UnitTests.Mocks;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace AloDoutor.Application.UnitTests.Features.Pacientes.Commands
{
    [Collection(nameof(PacienteTestsAutoMockerCollection))]
    public class AtualizarPacienteCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPacienteRepository> _mockPacienteRepo;
        private readonly AtualizarPacienteCommandHandler _pacienteHandler;
        private readonly PacienteTestsAutoMockerFixture _pacientefixture;
        private readonly AutoMocker _mocker;

        public AtualizarPacienteCommandHandlerTests(PacienteTestsAutoMockerFixture pacientefixture)
        {
            _pacientefixture = pacientefixture;
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<PacienteProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(mapperConfig.CreateMapper());
            _pacienteHandler = _mocker.CreateInstance<AtualizarPacienteCommandHandler>();
        }

        [Fact(DisplayName = "Atualizar paciente com Sucesso")]
        [Trait("Categoria", "Paciente - Paciente Command Handler")]
        public async Task AtualizarPaciente_NovoPaciente_DeveExecutarComSucesso()
        {
            // Arrange
            var pacienteOriginal = _pacientefixture.GerarPacienteValido();
            var pacienteUpdate = _pacientefixture.AtualizarPacienteCommand();
            var validator = new AtualizarPacienteCommandValidator();
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.Adicionar(It.IsAny<Paciente>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.ObterPorId(pacienteOriginal.Id)).ReturnsAsync(pacienteOriginal);

            //Act
            var validationResult = await validator.ValidateAsync(pacienteUpdate);
            pacienteUpdate.Id = pacienteOriginal.Id;
            var result = await _pacienteHandler.Handle(pacienteUpdate, CancellationToken.None);

            //Assert
            Assert.True(validationResult.IsValid);
            _mocker.GetMock<IPacienteRepository>().Verify(r => r.Atualizar(It.IsAny<Paciente>()), Times.Once);
            _mocker.GetMock<IPacienteRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);

        }
    }
}

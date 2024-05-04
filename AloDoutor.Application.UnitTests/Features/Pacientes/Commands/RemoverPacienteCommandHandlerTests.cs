using AloDoutor.Application.Features.Pacientes.Commands.RemoverPaciente;
using AloDoutor.Application.UnitTests.Mocks;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using Moq;
using Moq.AutoMock;

namespace AloDoutor.Application.UnitTests.Features.Pacientes.Commands
{
    [Collection(nameof(PacienteTestsAutoMockerCollection))]
    public class RemoverPacienteCommandHandlerTests
    {
        private readonly RemoverPacienteCommandHandler _pacienteHandler;
        private readonly PacienteTestsAutoMockerFixture _pacientefixture;
        private readonly AutoMocker _mocker;

        public RemoverPacienteCommandHandlerTests(PacienteTestsAutoMockerFixture pacientefixture)
        {
            _pacientefixture = pacientefixture; 
            _mocker = new AutoMocker();
            _pacienteHandler = _mocker.CreateInstance<RemoverPacienteCommandHandler>();
        }

        [Fact(DisplayName = "Excluir paciente com Sucesso")]
        [Trait("Categoria", "Paciente - Paciente Command Handler")]
        public async Task ExcluirPaciente_PacienteExistente_DeveExecutarComSucesso()
        {
            var pacienteOriginal = _pacientefixture.GerarPacienteValido();
            var paciente = new RemoverPacienteCommand();
            paciente.IdPaciente = pacienteOriginal.Id;
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.Adicionar(It.IsAny<Paciente>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.ObterPorId(pacienteOriginal.Id)).ReturnsAsync(pacienteOriginal);

            //Act            
            var result = await _pacienteHandler.Handle(paciente, CancellationToken.None);

            //Assert
            _mocker.GetMock<IPacienteRepository>().Verify(r => r.Remover(It.IsAny<Paciente>()), Times.Once);
            _mocker.GetMock<IPacienteRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}

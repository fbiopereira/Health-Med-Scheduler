using AloDoutor.Application.Features.Medicos.Commands.RemoverMedico;
using AloDoutor.Application.UnitTests.Mocks;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using Moq;
using Moq.AutoMock;

namespace AloDoutor.Application.UnitTests.Features.Medicos.Commands
{
    [Collection(nameof(MedicoTestsAutoMockerCollection))]
    public class RemoverMedicoCommandHandlerTests
    {
        private readonly RemoverMedicoCommandHandler _medicoHandler;
        private readonly MedicoTestsAutoMockerFixture _medicofixture;
        private readonly AutoMocker _mocker;

        public RemoverMedicoCommandHandlerTests(MedicoTestsAutoMockerFixture medicofixture)
        {
            _mocker = new AutoMocker();
            _medicoHandler = _mocker.CreateInstance<RemoverMedicoCommandHandler>();
            _medicofixture = medicofixture;
            _medicofixture = medicofixture;
        }

        [Fact(DisplayName = "Excluir medico com Sucesso")]
        [Trait("Categoria", "Medico - Medico Command Handler")]
        public async Task ExcluirMedico_MedicoExistente_DeveExecutarComSucesso()
        {
            var medicoOriginal = _medicofixture.GerarMedicoValido();
            var medico = new RemoverMedicoCommand();
            medico.IdMedico = medicoOriginal.Id;
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<Medico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.ObterPorId(medicoOriginal.Id)).ReturnsAsync(medicoOriginal);

            //Act            
            var result = await _medicoHandler.Handle(medico, CancellationToken.None);

            //Assert
            _mocker.GetMock<IMedicoRepository>().Verify(r => r.Remover(It.IsAny<Medico>()), Times.Once);
            _mocker.GetMock<IMedicoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
           
        }
}

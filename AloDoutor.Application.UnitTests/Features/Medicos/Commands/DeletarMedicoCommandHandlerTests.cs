using AloDoutor.Application.Features.Medicos.Commands.AdicionarMedico;
using AloDoutor.Application.Features.Medicos.Commands.AtualizarMedico;
using AloDoutor.Application.Features.Medicos.Commands.DeletarMedico;
using AloDoutor.Application.UnitTests.Mocks;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;
using Shouldly;

namespace AloDoutor.Application.UnitTests.Features.Medicos.Commands
{
    [Collection(nameof(MedicoTestsAutoMockerCollection))]
    public class DeletarMedicoCommandHandlerTests
    {
        private readonly DeletarMedicoCommandHandler _medicoHandler;
        private readonly MedicoTestsAutoMockerFixture _medicofixture;
        private readonly AutoMocker _mocker;

        public DeletarMedicoCommandHandlerTests(MedicoTestsAutoMockerFixture medicofixture)
        {
            _mocker = new AutoMocker();
            _medicoHandler = _mocker.CreateInstance<DeletarMedicoCommandHandler>();
            _medicofixture = medicofixture;
            _medicofixture = medicofixture;
        }

        [Fact(DisplayName = "Excluir medico com Sucesso")]
        [Trait("Categoria", "Excluir Medico - Medico Command Handler")]
        public async Task ExcluirMedico_MedicoExistente_DeveExecutarComSucesso()
        {
            var medicoOriginal = _medicofixture.GerarMedicoValido();
            var medico = new DeletarMedicoCommand();
            medico.Id = medicoOriginal.Id;
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

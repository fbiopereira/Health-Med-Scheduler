using AloDoutor.Application.Features.Especialidades.Commands.RemoverEspecialidade;
using AloDoutor.Application.Features.Medicos.Commands.RemoverMedico;
using AloDoutor.Application.UnitTests.Mocks;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Application.UnitTests.Features.Especialidades.Commands
{
    public class RemoverEspecialidadeCommandHandlerTests
    {
        private readonly RemoverEspecialidadeCommandHandler _especialidadeHandler;
        private readonly AutoMocker _mocker;

        public RemoverEspecialidadeCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _especialidadeHandler = _mocker.CreateInstance<RemoverEspecialidadeCommandHandler>();
        }

        [Fact(DisplayName = "Excluir Especialidade com Sucesso")]
        [Trait("Categoria", "Especialidade - Especialidade Command Handler")]
        public async Task ExcluirEspecialidade_EspecialidadeExistente_DeveExecutarComSucesso()
        {
            var especialidadeOriginal = new Especialidade("Pediatra", "Atendimento de segunda a sexta");
            var especialidade = new RemoverEspecialidadeCommand();
            especialidade.idEspecialidade = especialidadeOriginal.Id;
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.Adicionar(It.IsAny<Especialidade>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.ObterPorId(especialidadeOriginal.Id)).ReturnsAsync(especialidadeOriginal);

            //Act            
            var result = await _especialidadeHandler.Handle(especialidade, CancellationToken.None);

            //Assert
            _mocker.GetMock<IEspecialidadeRepository>().Verify(r => r.Remover(It.IsAny<Especialidade>()), Times.Once);
            _mocker.GetMock<IEspecialidadeRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}

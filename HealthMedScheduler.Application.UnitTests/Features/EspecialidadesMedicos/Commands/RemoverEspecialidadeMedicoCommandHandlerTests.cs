using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.RemoverEspecialidadeMedico;
using HealthMedScheduler.Application.UnitTests.Mocks;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.EspecialidadesMedicos.Commands
{
    [Collection(nameof(MedicoTestsAutoMockerCollection))]
    public class RemoverEspecialidadeMedicoCommandHandlerTests
    {

        private readonly RemoverEspecialidadeMedicoCommandHandler _especialidadeMedicoHandler;
        private readonly AutoMocker _mocker;
        private readonly MedicoTestsAutoMockerFixture _medicofixture;

        public RemoverEspecialidadeMedicoCommandHandlerTests(MedicoTestsAutoMockerFixture medicofixture)
        {
            _mocker = new AutoMocker();
            _especialidadeMedicoHandler = _mocker.CreateInstance<RemoverEspecialidadeMedicoCommandHandler>();
            _medicofixture = medicofixture;
        }

        [Fact(DisplayName = "Excluir Especialidade Medico com Sucesso")]
        [Trait("Categoria", "Especialidade Medico - Especialidade Medico Command Handler")]
        public async Task ExcluirAgendamento_MedicoAgendamento_DeveExecutarComSucesso()
        {
            var especialidade = new Especialidade("Pediatra", "Atendimento de segunda a sexta!");
            var medico = _medicofixture.GerarMedicoValido();
            var especialidadeMedico = new EspecialidadeMedico(especialidade.Id, medico.Id, DateTime.Parse("2024-03-05"));

            var especialidadeMedicoCommand = new RemoverEspecialidadeMedicoCommand
            {
                IdEspecialdiadeMedico = especialidadeMedico.Id,
            };
            var validator = new RemoverEspecialidadeMedicoCommandValidator();

            //EspecialidadeMedico
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<EspecialidadeMedico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.ObterPorId(especialidadeMedico.Id)).ReturnsAsync(especialidadeMedico);

            //Act
            var validationResult = await validator.ValidateAsync(especialidadeMedicoCommand);
            var result = await _especialidadeMedicoHandler.Handle(especialidadeMedicoCommand, CancellationToken.None);

            //Assert
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Verify(r => r.Remover(It.IsAny<EspecialidadeMedico>()), Times.Once);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}

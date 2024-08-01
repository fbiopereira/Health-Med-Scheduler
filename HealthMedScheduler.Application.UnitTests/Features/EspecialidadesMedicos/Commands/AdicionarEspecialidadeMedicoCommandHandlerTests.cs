using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Application.UnitTests.Mocks;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.EspecialidadesMedicos.Commands
{
    [Collection(nameof(MedicoTestsAutoMockerCollection))]
    public class AdicionarEspecialidadeMedicoCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly AdicionarEspecialidadeMedicoCommandHandler _especialidadeMedicoHandler;
        private readonly MedicoTestsAutoMockerFixture _medicofixture;
        private readonly AutoMocker _mocker;

        public AdicionarEspecialidadeMedicoCommandHandlerTests(MedicoTestsAutoMockerFixture medicofixture)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<EspecialidadeMedicoProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(_mapper);
            _especialidadeMedicoHandler = _mocker.CreateInstance<AdicionarEspecialidadeMedicoCommandHandler>();
            _medicofixture = medicofixture;
        }
        [Fact(DisplayName = "Adicionar Especialidade Medico Nova Especialidade Medico com Sucesso")]
        [Trait("Categoria", "Especialidade Medico - Especialidade Command Handler")]
        public async Task AdicionarEspecialidadeMedico_NovaEspecialidadeMedico_DeveExecutarComSucesso()
        {
            //Arrange
            var medico = _medicofixture.GerarMedicoValido();
            var especialidade = new Especialidade("Pediatra", "Atende de segunda a sexta");
            var especialidadeMedicoCommand = new AdicionarEspecialidadeMedicoCommand
            {
                EspecialidadeId = especialidade.Id,
                MedicoId = medico.Id,
                DataRegistro = DateTime.Parse("2024-05-03")
            };

            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<Medico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.ObterPorId(medico.Id)).ReturnsAsync(medico);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.Adicionar(It.IsAny<Especialidade>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.ObterPorId(especialidade.Id)).ReturnsAsync(especialidade);

            var validator = new AdicionarEspecialidadeMedicoCommandValidator();
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _especialidadeMedicoHandler.Handle(especialidadeMedicoCommand, CancellationToken.None);
            var validationResult = await validator.ValidateAsync(especialidadeMedicoCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Verify(r => r.Adicionar(It.IsAny<EspecialidadeMedico>()), Times.Once);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}

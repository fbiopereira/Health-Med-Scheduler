using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AtualizarEspecialidadeMedico;
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
    public class AtualizarEspecialidadeMedicoCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly AtualizarEspecialidadeMedicoCommandHandler _especialidadeMedicoHandler;
        private readonly AutoMocker _mocker;
        private readonly MedicoTestsAutoMockerFixture _medicofixture;

        public AtualizarEspecialidadeMedicoCommandHandlerTests(MedicoTestsAutoMockerFixture medicofixture)
        {
            _medicofixture = medicofixture;
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<EspecialidadeMedicoProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(mapperConfig.CreateMapper());
            _especialidadeMedicoHandler = _mocker.CreateInstance<AtualizarEspecialidadeMedicoCommandHandler>();
        }

        [Fact(DisplayName = "Atualizar Especialidade com Sucesso")]
        [Trait("Categoria", "Especialidade Medico - Especialidade Command Handler")]
        public async Task AtualizarEspecialidade_NovaEspecialidade_DeveExecutarComSucesso()
        {
            var especialidade = new Especialidade("Pediatra", "Atendimento de segunda a sexta!");
            var medico = _medicofixture.GerarMedicoValido();
            var especialidadeMedico = new EspecialidadeMedico(especialidade.Id, medico.Id, DateTime.Parse("2024-03-05"));

            var especialidadeMedicoCommand = new AtualizarEspecialidadeMedicoCommand
            {
                Id = especialidadeMedico.Id,
                EspecialidadeId = especialidadeMedico.EspecialidadeId,
                MedicoId = especialidadeMedico.MedicoId,
                DataRegistro = DateTime.Parse("2025-03-05")
            };
            var validator = new AtualizarEspecialidadeMedicoCommandValidator();

            //Paciente
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<EspecialidadeMedico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.ObterPorId(especialidadeMedico.Id)).ReturnsAsync(especialidadeMedico);
            
            //Especialidade
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.Adicionar(It.IsAny<Especialidade>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.ObterPorId(especialidade.Id)).ReturnsAsync(especialidade);

            //EspecialidadeMedico
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<Medico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.ObterPorId(medico.Id)).ReturnsAsync(medico);

            //Act
            var validationResult = await validator.ValidateAsync(especialidadeMedicoCommand);
            var result = await _especialidadeMedicoHandler.Handle(especialidadeMedicoCommand, CancellationToken.None);

            //Assert
            Assert.True(validationResult.IsValid);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Verify(r => r.Atualizar(It.IsAny<EspecialidadeMedico>()), Times.Once);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);

        }
    }
}

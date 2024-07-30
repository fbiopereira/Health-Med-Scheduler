using HealthMedScheduler.Application.Features.Especialidades.Commands.AdicionarEspecialidade;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.Especialidades.Commands
{
    public class AdicionarEspecialidadeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEspecialidadeRepository> _mockEspecialidadeRepo;
        private readonly AdicionarEspecialidadeCommandHandler _especialidadeHandler;
        private readonly AutoMocker _mocker;

        public AdicionarEspecialidadeCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<EspecialidadeProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(_mapper);
            _especialidadeHandler = _mocker.CreateInstance<AdicionarEspecialidadeCommandHandler>();
        }

        [Fact(DisplayName = "Adicionar Especialidade Nova Especialidade com Sucesso")]
        [Trait("Categoria", "Especialidade - Especialidade Command Handler")]
        public async Task AdicionarEspecialidade_NovaEspecialidade_DeveExecutarComSucesso()
        {
            //Arrange
            var especialidadeCommand = new AdicionarEspecialidadeCommand
            {
                Nome = "Pediatra",
                Descricao = "Atendimento de segunda a sexta"
            };
            var validator = new AdicionarEspecialidadeCommandValidator();
            _mocker.GetMock<IEspecialidadeRepository>().Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _especialidadeHandler.Handle(especialidadeCommand, CancellationToken.None);
            var validationResult = await validator.ValidateAsync(especialidadeCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            _mocker.GetMock<IEspecialidadeRepository>().Verify(r => r.Adicionar(It.IsAny<Especialidade>()), Times.Once);
            _mocker.GetMock<IEspecialidadeRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar nova especialidade com Fallha")]
        [Trait("Categoria", "Especialidade - Especialidade Command Handler")]
        public async Task AdicionarEspecialidade_NovaEspecialdiade_DeveExecutarComFalha()
        {
            //Arrange
            var especialidadeCommand = new AdicionarEspecialidadeCommand
            {
                Nome = "",
                Descricao = ""
            };
            var validator = new AdicionarEspecialidadeCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(especialidadeCommand);

            //Acert
            Assert.False(validationResult.IsValid);
        }
    }
}
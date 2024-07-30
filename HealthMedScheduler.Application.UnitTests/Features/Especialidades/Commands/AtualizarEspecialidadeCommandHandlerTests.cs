using HealthMedScheduler.Application.Features.Especialidades.Commands.AtualizarEspecialidade;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.Especialidades.Commands
{
    public class AtualizarEspecialidadeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly AtualizarEspecialidadeCommandHandler _especialidadeHandler;
        private readonly AutoMocker _mocker;

        public AtualizarEspecialidadeCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<EspecialidadeProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(_mapper);
            _especialidadeHandler = _mocker.CreateInstance<AtualizarEspecialidadeCommandHandler>();
        }

        [Fact(DisplayName = "Atualizar Especialidade com Sucesso")]
        [Trait("Categoria", "Especialidade - Especialidade Command Handler")]
        public async Task AtualizarEspecialidade_NovaEspecialidade_DeveExecutarComSucesso()
        {
            // Arrange
            var especialidadeOriginal = new Especialidade("Clinico Geral", "Atendimento de segunda a sexta");
            var medicoUpdate = new AtualizarEspecialidadeCommand
            {
                Id = especialidadeOriginal.Id,
                Nome = "Pediatra",
                Descricao = "Atendimento de segunda a quinta"
            };
            var validator = new AtualizarEspecialidadeCommandValidator();
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.Adicionar(It.IsAny<Especialidade>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.ObterPorId(especialidadeOriginal.Id)).ReturnsAsync(especialidadeOriginal);

            //Act
            var validationResult = await validator.ValidateAsync(medicoUpdate);
            medicoUpdate.Id = especialidadeOriginal.Id;
            var result = await _especialidadeHandler.Handle(medicoUpdate, CancellationToken.None);

            //Assert
            Assert.True(validationResult.IsValid);
            _mocker.GetMock<IEspecialidadeRepository>().Verify(r => r.Atualizar(It.IsAny<Especialidade>()), Times.Once);
            _mocker.GetMock<IEspecialidadeRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}

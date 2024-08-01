using HealthMedScheduler.Application.Features.Medicos.Commands.AtualizarMedico;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Application.UnitTests.Mocks;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.Medicos.Commands
{
    [Collection(nameof(MedicoTestsAutoMockerCollection))]
    public class AtualizarMedicoCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAgendaMedicoRepository> _mockMedicoRepo;
        private readonly AtualizarMedicoCommandHandler _medicoHandler;
        private readonly MedicoTestsAutoMockerFixture _medicofixture;
        private readonly AutoMocker _mocker;

        public AtualizarMedicoCommandHandlerTests(MedicoTestsAutoMockerFixture medicofixture)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MedicoProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(mapperConfig.CreateMapper());
            _medicoHandler = _mocker.CreateInstance<AtualizarMedicoCommandHandler>();
            _medicofixture = medicofixture;           
        }

        [Fact(DisplayName = "Atualizar medico com Sucesso")]
        [Trait("Categoria", "Medico - Medico Command Handler")]
        public async Task AtualizarMedico_NovoMedico_DeveExecutarComSucesso()
        {          
            // Arrange
            var medicoOriginal = _medicofixture.GerarMedicoValido();
            var medicoUpdate = _medicofixture.AtualizarMedicoCommand();
            var validator = new AtualizarMedicoCommandValidator();
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<Medico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.ObterPorId(medicoOriginal.Id)).ReturnsAsync(medicoOriginal);

            //Act
            var validationResult = await validator.ValidateAsync(medicoUpdate);
            medicoUpdate.Id = medicoOriginal.Id;
            var result = await _medicoHandler.Handle(medicoUpdate, CancellationToken.None);

            //Assert
            Assert.True(validationResult.IsValid);
            _mocker.GetMock<IMedicoRepository>().Verify(r => r.Atualizar(It.IsAny<Medico>()), Times.Once);
            _mocker.GetMock<IMedicoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);

        }       

        /* [Fact]
         public async Task AtualizarMedico_DeveRetornarListaDeMedicos()
         {

             var handler = new AtualizarMedicoCommandHandler(_mapper, _mockMedicoRepo.Object);

             await handler.Handle(new AtualizarMedicoCommand() { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Cpf = "82841218082", Nome = "Medico 1", Cep = "11111111111", Telefone = "2222-2222", Endereco = "Rua Fake 9999", Estado = "Fake", Crm = "AA-12345" }, CancellationToken.None);

             var medicos = await _mockMedicoRepo.Object.ObterTodos();
             medicos.Count.ShouldBe(3);
         }*/
    }
}

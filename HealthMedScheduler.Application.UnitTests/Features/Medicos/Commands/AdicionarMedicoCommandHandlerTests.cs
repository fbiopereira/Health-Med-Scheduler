using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarMedico;
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
    public class AdicionarMedicoCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAgendaMedicoRepository> _mockMedicoRepo;
        private readonly AdicionarMedicoCommandHandler _medicoHandler;
        private readonly MedicoTestsAutoMockerFixture _medicofixture;
        private readonly AutoMocker _mocker;

        public AdicionarMedicoCommandHandlerTests(MedicoTestsAutoMockerFixture medicofixture)
        {

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MedicoProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(mapperConfig.CreateMapper());
            _medicoHandler = _mocker.CreateInstance<AdicionarMedicoCommandHandler>();
            _medicofixture = medicofixture;
        }

        //[Fact(DisplayName = "Adicionar medico Novo Medico com Sucesso")]
        //[Trait("Categoria", "Medico - Medico Command Handler")]
        //public async Task AdicionarMedico_NovoMedico_DeveExecutarComSucesso()
        //{
        //    //Arrange
        //    var medicoCommand = _medicofixture.GerarMedicoCommandValido();
        //    var validator = new AdicionarMedicoCommandValidator();
        //    _mocker.GetMock<IMedicoRepository>().Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));
           
        //    //Act
        //    var result = await _medicoHandler.Handle(medicoCommand, CancellationToken.None);
        //    var validationResult = await validator.ValidateAsync(medicoCommand);

        //    //Assert
        //    Assert.True(validationResult.IsValid);
        //    _mocker.GetMock<IMedicoRepository>().Verify(r => r.Adicionar(It.IsAny<Medico>()), Times.Once);
        //    _mocker.GetMock<IMedicoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        //}

        [Fact(DisplayName = "Adicionar novo medico com Fallha")]
        [Trait("Categoria", "Medico - Medico Command Handler")]
        public async Task AdicionarMedico_CPFMedicoExistente_DeveExecutarComFalha()
        {
            // Arrange
            var medicoOriginal = _medicofixture.GerarMedicoValido();
            var medicoNovo = _medicofixture.GerarMedicoCommandValido();
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<Medico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.ObterMedicoPorCPF(medicoOriginal.Cpf)).ReturnsAsync(medicoOriginal);

            //Act
            medicoNovo.Cpf = medicoOriginal.Cpf;
            //var result = await _medicoHandler.Handle(medicoNovo, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _medicoHandler.Handle(medicoNovo, CancellationToken.None));
            _mocker.GetMock<IAgendaMedicoRepository>().Verify(r => r.Adicionar(It.IsAny<Medico>()), Times.Never);
            _mocker.GetMock<IAgendaMedicoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);

        }

        [Fact(DisplayName = "Adicionar medico Novo Medico com Falha")]
        [Trait("Categoria", "Medico - Medico Command Handler")]
        public async Task AdicionarMedico_NovoMedico_DeveExecutarComFalha()
        {
            //Arrange
            var medicoCommand = _medicofixture.GerarMedicoCommandInValido();
            var validator = new AdicionarMedicoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(medicoCommand);

            //Acert
            Assert.False(validationResult.IsValid);            
        }

        /* [Fact]
         public async Task AdicionarMedico_DeveRetornarListaDeMedicos()
         {
             var handler = new AdicionarMedicoCommandHandler(_mapper, _mockMedicoRepo.Object);

             await handler.Handle(new AdicionarMedicoCommand() { Cpf = "82841218082", Nome = "Medico 1", Cep = "11111111111", Telefone = "2222-2222", Endereco = "Rua Fake", Estado = "Fake", Crm = "AA-12345" }, CancellationToken.None);

             var medicos = await _mockMedicoRepo.Object.ObterTodos();
             medicos.Count.ShouldBe(4);
         }*/
    }
}

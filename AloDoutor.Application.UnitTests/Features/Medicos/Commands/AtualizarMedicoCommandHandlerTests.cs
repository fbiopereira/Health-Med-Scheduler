using AloDoutor.Application.Features.Medicos.Commands.AtualizarMedico;
using AloDoutor.Application.MappingProfiles;
using AloDoutor.Application.UnitTests.Mocks;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace AloDoutor.Application.UnitTests.Features.Medicos.Commands
{
    public class AtualizarMedicoCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMedicoRepository> _mockMedicoRepo;
        private readonly Mock<ILogger<AtualizarMedicoCommandHandler>> _mockLogger;

        public AtualizarMedicoCommandHandlerTests()
        {
            _mockMedicoRepo = MockMedicoRepository.ObterTodosMockMedicoRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MedicoProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockLogger = new Mock<ILogger<AtualizarMedicoCommandHandler>>();
        }

        [Fact]
        public async Task AtualizarMedico_DeveRetornarListaDeMedicos()
        {
            
            var handler = new AtualizarMedicoCommandHandler(_mapper, _mockMedicoRepo.Object, _mockLogger.Object);

            await handler.Handle(new AtualizarMedicoCommand() { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Cpf = "82841218082", Nome = "Medico 1", Cep = "11111111111", Telefone = "2222-2222", Endereco = "Rua Fake 9999", Estado = "Fake", Crm = "AA-12345" }, CancellationToken.None);

            var medicos = await _mockMedicoRepo.Object.ObterTodos();
            medicos.Count.ShouldBe(3);
        }
    }
}

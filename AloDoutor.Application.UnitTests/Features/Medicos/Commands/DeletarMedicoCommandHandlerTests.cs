using AloDoutor.Application.Features.Medicos.Commands.DeletarMedico;
using AloDoutor.Application.Interfaces;
using AloDoutor.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace AloDoutor.Application.UnitTests.Features.Medicos.Commands
{
    public class DeletarMedicoCommandHandlerTests
    {
        private readonly Mock<IMedicoRepository> _mockMedicoRepo;

        public DeletarMedicoCommandHandlerTests()
        {
            _mockMedicoRepo = MockMedicoRepository.ObterTodosMockMedicoRepository();
        }

        [Fact]
        public async Task DeletarMedico_DeveRetornarUnitValue_ParaSucesso()
        {
            var handler = new DeletarMedicoCommandHandler(_mockMedicoRepo.Object);

            await handler.Handle(new DeletarMedicoCommand() { Id = Guid.Parse("11111111-1111-1111-1111-111111111111") }, CancellationToken.None);

            var medicos = await _mockMedicoRepo.Object.ObterTodos();
            medicos.Count.ShouldBe(2);
        }
    }
}

using AloDoutor.Application.Features.Medicos.Commands.AdicionarMedico;
using AloDoutor.Application.Interfaces;
using AloDoutor.Application.MappingProfiles;
using AloDoutor.Application.UnitTests.Mocks;
using AloDoutor.Domain.Entity;
using AutoMapper;
using Moq;
using Shouldly;

namespace AloDoutor.Application.UnitTests.Features.Medicos.Commands
{
    public class AdicionarMedicoCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMedicoRepository> _mockMedicoRepo;

        public AdicionarMedicoCommandHandlerTests()
        {
            _mockMedicoRepo = MockMedicoRepository.ObterTodosMockMedicoRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MedicoProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task AdicionarMedico_DeveRetornarListaDeMedicos()
        {
            var handler = new AdicionarMedicoCommandHandler(_mapper, _mockMedicoRepo.Object);

            await handler.Handle(new AdicionarMedicoCommand() { Cpf = "82841218082", Nome = "Medico 1", Cep = "11111111111", Telefone = "2222-2222", Endereco = "Rua Fake", Estado = "Fake", Crm = "AA-12345" }, CancellationToken.None);     

            var medicos = await _mockMedicoRepo.Object.ObterTodos();
            medicos.Count.ShouldBe(4);
        }
    }
}

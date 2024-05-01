using AloDoutor.Application.Features.Medicos.Queries.ObterTodosMedicos;
using AloDoutor.Application.MappingProfiles;
using AloDoutor.Application.UnitTests.Mocks;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace AloDoutor.Application.UnitTests.Features.Medicos.Queries
{
   /* public class ObterMedicosQueryHandlerTests
    {
        private readonly Mock<IMedicoRepository> _medicoRepository;
        private IMapper _mapper;
        private readonly Mock<ILogger<ObterMedicosQueryHandler>> _logger;

        public ObterMedicosQueryHandlerTests()
        {
            _medicoRepository = MockMedicoRepository.ObterTodosMockMedicoRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MedicoProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<ObterMedicosQueryHandler>>();

        }

        [Fact]
        public async Task ObterMedicosQueryHandler_DeveRetornarListaDeMedicos()
        {
            //Arrange   
            var handler = new ObterMedicosQueryHandler(_mapper, _medicoRepository.Object, _logger.Object);

            //Act
            var result = await handler.Handle(new ObterMedicosQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<List<MedicoDTO>>();
            result.Count.ShouldBe(3);
        }
    }*/
}

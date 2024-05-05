using AloDoutor.Application.Features.Agendamentos.Queries;
using AloDoutor.Application.Features.EspecialidadesMedicos.Queries;
using AloDoutor.Application.MappingProfiles;
using AloDoutor.Application.ViewModel;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace AloDoutor.Application.UnitTests.Features.Agendamentos.Queries
{
    public class ObterAgendamentoQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ObterAgendamentoQueryHandler _agendamentoHandler;
        private readonly AutoMocker _mocker;

        public ObterAgendamentoQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AgendamentoProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(_mapper);
            _agendamentoHandler = _mocker.CreateInstance<ObterAgendamentoQueryHandler>();
        }

        [Fact(DisplayName = "Obter Lista Todos agendamentos com Sucesso")]
        [Trait("Categoria", "Agendamento - Agendamento Query Handler")]
        public async Task Handler_DeveRetornarListaDeAgendamentos_DeveExecutarComSucesso()
        {
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.ObterTodos()).ReturnsAsync(new List<Agendamento>());

            var query = new ObterAgendamentoQuery();

            // Act
            var resultado = await _agendamentoHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<IEnumerable<AgendamentoViewModel>>(resultado);
        }

        [Fact(DisplayName = "Obter Agendamento Por Id com Sucesso")]
        [Trait("Categoria", "Agendamento - Agendamento Query Handler")]
        public async Task Handler_DeveRetornarAgendamentoPorId_DeveExecutarComSucesso()
        {
            var idAgendamento = Guid.NewGuid();
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.ObterPorId(idAgendamento)).ReturnsAsync(new Agendamento());

            var query = new ObterAgendamentoPorIdQuery(idAgendamento);

            // Act
            var resultado = await _agendamentoHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<AgendamentoViewModel>(resultado);
        }

        [Fact(DisplayName = "Obter Agendamentos Por Status com Sucesso")]
        [Trait("Categoria", "Agendamento - Agendamento Query Handler")]
        public async Task Handler_DeveRetornarAgendamentosPorStatus_DeveExecutarComSucesso()
        {
            var status = 1;
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.ObterAgendamentosPorIStatus(status)).ReturnsAsync(new List<Agendamento>());

            var query = new ObterAgendamentoPorStatusQuery(status);

            // Act
            var resultado = await _agendamentoHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<IEnumerable<AgendamentoViewModel>>(resultado);
        }
    }
}

using HealthMedScheduler.Application.Features.Especialidades.Queries;
using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Queries;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Application.ViewModel;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.Especialidades.Queries
{
    public class ObterEspecialidadeQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ObterEspecialidadeQueryHandler _especialidadeHandler;
        private readonly AutoMocker _mocker;

        public ObterEspecialidadeQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<EspecialidadeProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(_mapper);
            _especialidadeHandler = _mocker.CreateInstance<ObterEspecialidadeQueryHandler>();
        }

        [Fact(DisplayName = "Obter Lista Todas Especialidades com Sucesso")]
        [Trait("Categoria", "Especialidade - Especialidade Query Handler")]
        public async Task Handler_DeveRetornarListaDeEspecialidade_DeveExecutarComSucesso()
        {
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.ObterTodos()).ReturnsAsync(new List<Especialidade>());

            var query = new ObterEspecialidadeQuery();

            // Act
            var resultado = await _especialidadeHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<IEnumerable<EspecialidadeViewModel>>(resultado);
        }

        [Fact(DisplayName = "Obter Especialidades Por Id com Sucesso")]
        [Trait("Categoria", "Especialidade - Especialidade Query Handler")]
        public async Task Handler_DeveRetornarEspecialidadePorId_DeveExecutarComSucesso()
        {
            var idEspecialidade = Guid.NewGuid();
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.ObterPorId(idEspecialidade)).ReturnsAsync(new Especialidade());

            var query = new ObterEspecialidadePorIdQuery(idEspecialidade);

            // Act
            var resultado = await _especialidadeHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<EspecialidadeViewModel>(resultado);
        }

        [Fact(DisplayName = "Obter Especialidades Medicos Por Id com Sucesso")]
        [Trait("Categoria", "Especialidade - Especialidade Query Handler")]
        public async Task Handler_DeveRetornarEspecialidadeMedicosPorId_DeveExecutarComSucesso()
        {
            var idEspecialidade = Guid.NewGuid();
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.ObterMedicosPorEspecialidadeId(idEspecialidade)).ReturnsAsync(new Especialidade());

            var query = new ObterEspecialidadeMedicosPorIdQuery(idEspecialidade);

            // Act
            var resultado = await _especialidadeHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<EspecialidadeViewModel>(resultado);
        }
    }
}

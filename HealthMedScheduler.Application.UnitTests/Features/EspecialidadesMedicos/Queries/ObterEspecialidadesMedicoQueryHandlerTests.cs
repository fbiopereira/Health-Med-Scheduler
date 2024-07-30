using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Queries;
using HealthMedScheduler.Application.Features.Medicos.Queries.ObterTodosMedicos;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Application.ViewModel;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.EspecialidadesMedicos.Queries
{
    public class ObterEspecialidadesMedicoQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ObterEspecialidadesMedicoQueryHandler _especialidadeMedicoHandler;
        private readonly AutoMocker _mocker;

        public ObterEspecialidadesMedicoQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<EspecialidadeMedicoProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(_mapper);
            _especialidadeMedicoHandler = _mocker.CreateInstance<ObterEspecialidadesMedicoQueryHandler>();
        }

        [Fact(DisplayName = "Obter Lista Todas Especialidades Medicos com Sucesso")]
        [Trait("Categoria", "Especialidade Medico - Especialidade Medico Query Handler")]
        public async Task Handler_DeveRetornarListaDeEspecialidadeMedicos_DeveExecutarComSucesso()
        {
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.ObterTodos()).ReturnsAsync(new List<EspecialidadeMedico>());

            var query = new ObterEspecialidadeMedicoQuery();

            // Act
            var resultado = await _especialidadeMedicoHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<IEnumerable<EspecialidadeMedicosViewModel>>(resultado); 
        }

        [Fact(DisplayName = "Obter Especialidade Medico Por Id com Sucesso")]
        [Trait("Categoria", "Especialidade Medico - Especialidade Medico Query Handler")]
        public async Task Handler_DeveRetornarEspecialidadeMedicoPorId_DeveExecutarComSucesso()
        {
            var idEspecialidade = Guid.NewGuid();
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.ObterPorId(idEspecialidade)).ReturnsAsync(new EspecialidadeMedico());

            var query = new ObterMedicoEspecialidadePorIdQuery(idEspecialidade);

            // Act
            var resultado = await _especialidadeMedicoHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<EspecialidadeMedicosViewModel>(resultado);
        }
    }
}

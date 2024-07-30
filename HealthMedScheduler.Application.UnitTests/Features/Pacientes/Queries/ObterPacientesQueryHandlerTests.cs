using HealthMedScheduler.Application.Features.Pacientes.Queries;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Application.ViewModel;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.Pacientes.Queries
{
    public class ObterPacientesQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ObterPacientesQueryHandler _pacienteHandler;
        private readonly AutoMocker _mocker;

        public ObterPacientesQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<PacienteProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(_mapper);
            _pacienteHandler = _mocker.CreateInstance<ObterPacientesQueryHandler>();
        }

        [Fact(DisplayName = "Obter Lista Todos Pacientes com Sucesso")]
        [Trait("Categoria", "Paciente - Paciente Query Handler")]
        public async Task Handler_DeveRetornarListaDePacientes_DeveExecutarComSucesso()
        {
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.ObterTodos()).ReturnsAsync(new List<Paciente>());

            var query = new ObterPacientesQuery();

            // Act
            var resultado = await _pacienteHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado); // Verifica se o resultado não é nulo
            Assert.IsAssignableFrom<IEnumerable<PacienteViewModel>>(resultado); // Verifica se o resultado é uma coleção de PacienteViewModel
        }

        [Fact(DisplayName = "Obter Paciente Por id com Sucesso")]
        [Trait("Categoria", "Paciente - Paciente Query Handler")]
        public async Task Handler_DeveRetornarPacientePorId_DeveExecutarComSucesso()
        {
            var idPaciente = Guid.NewGuid();
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.ObterPorId(idPaciente)).ReturnsAsync(new Paciente());

            var query = new ObterPacientePorIdQuery(idPaciente);

            // Act
            var resultado = await _pacienteHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado); 
            Assert.IsAssignableFrom<PacienteViewModel>(resultado); 

        }

        [Fact(DisplayName = "Obter Agendamentos Paciente Por id com Sucesso")]
        [Trait("Categoria", "Paciente - Paciente Query Handler")]
        public async Task Handler_DeveRetornarAgendaPacientePorId_DeveExecutarComSucesso()
        {
            var idPaciente = Guid.NewGuid();
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.ObterAgendamentosPorIdPaciente(idPaciente)).ReturnsAsync(new Paciente());

            var query = new ObterAgendamentoPacientePorIdQuery(idPaciente);

            // Act
            var resultado = await _pacienteHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<PacienteViewModel>(resultado);

        }
    }
}

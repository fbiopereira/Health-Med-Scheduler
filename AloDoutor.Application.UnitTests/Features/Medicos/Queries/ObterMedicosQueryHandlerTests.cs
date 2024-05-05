using AloDoutor.Application.Features.Medicos.Queries.ObterTodosMedicos;
using AloDoutor.Application.MappingProfiles;
using AloDoutor.Application.ViewModel;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;

namespace AloDoutor.Application.UnitTests.Features.Medicos.Queries
{
    public class ObterMedicosQueryHandlerTests
     {
        private readonly IMapper _mapper;
        private readonly ObterMedicosQueryHandler _medicoHandler;
        private readonly AutoMocker _mocker;

        public ObterMedicosQueryHandlerTests()
         {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MedicoProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(_mapper);
            _medicoHandler = _mocker.CreateInstance<ObterMedicosQueryHandler>();

        }

        [Fact(DisplayName = "Obter Lista Todos Medicos com Sucesso")]
        [Trait("Categoria", "Medico - Medico Query Handler")]
        public async Task Handler_DeveRetornarListaDeMedicos_DeveExecutarComSucesso()
         {
            var medicos = new List<MedicoViewModel> { new MedicoViewModel { /* Propriedades do médico */ } };
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.ObterTodos()).ReturnsAsync(new List<Medico>());
           
            var query = new ObterMedicosQuery();

            // Act
            var resultado = await _medicoHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado); // Verifica se o resultado não é nulo
            Assert.IsAssignableFrom<IEnumerable<MedicoViewModel>>(resultado); // Verifica se o resultado é uma coleção de MedicoViewModel
        }


        [Fact(DisplayName = "Obte Medico Por id com Sucesso")]
        [Trait("Categoria", "Medico - Medico Query Handler")]
        public async Task Handler_DeveRetornarMedicoPorId_DeveExecutarComSucesso()
        {
            var idMedico = Guid.NewGuid();
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.ObterPorId(idMedico)).ReturnsAsync(new Medico());

            var query = new ObterMedicoPorIdQuery(idMedico);

            // Act
            var resultado = await _medicoHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado); // Verifica se o resultado não é nulo
            Assert.IsAssignableFrom<MedicoViewModel>(resultado); // Verifica se o resultado é uma coleção de MedicoViewModel

        }
        [Fact(DisplayName = "Obte Especialidades Medico Por id com Sucesso")]
        [Trait("Categoria", "Medico - Medico Query Handler")]
        public async Task Handler_DeveRetornarEspecialidadesMedicoPorId_DeveExecutarComSucesso()
        {
            var idMedico = Guid.NewGuid();
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.ObterEspecialidadesPorIdMedico(idMedico)).ReturnsAsync(new Medico());

            var query = new ObterEspecialidadePorIdMedicoQuery(idMedico);

            // Act
            var resultado = await _medicoHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado); // Verifica se o resultado não é nulo
            Assert.IsAssignableFrom<MedicoViewModel>(resultado); // Verifica se o resultado é uma coleção de MedicoViewModel
        }

        [Fact(DisplayName = "Obte Agendamnetos Medico Por id com Sucesso")]
        [Trait("Categoria", "Medico - Medico Query Handler")]
        public async Task Handler_DeveRetornarAgendamentosMedicoPorId_DeveExecutarComSucesso()
        {
            var idMedico = Guid.NewGuid();
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.ObterAgendamentosPorIdMedico(idMedico)).ReturnsAsync(new Medico());

            var query = new ObterAgendamentoMedicoPorIdMedicoQuery(idMedico);

            // Act
            var resultado = await _medicoHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado); // Verifica se o resultado não é nulo
            Assert.IsAssignableFrom<MedicoViewModel>(resultado); // Verifica se o resultado é uma coleção de MedicoViewModel
        }
    }
}

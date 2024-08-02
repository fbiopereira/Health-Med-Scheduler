using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Application.Features.Agendamentos.Commands.AdicionarAgendamento;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Application.UnitTests.Mocks;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using Moq;
using Moq.AutoMock;
using HealthMedScheduler.Application.Features.Especialidades.Commands.AdicionarEspecialidade;

namespace HealthMedScheduler.Application.UnitTests.Features.Agendamentos.Commands
{
    [Collection(nameof(PacienteTestsAutoMockerCollection))]
    public class AdicionarAgendamentoCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAgendamentoRepository> _mockAgendamentoRepo;
        private readonly AdicionarAgendamentoCommandHandler _agendamentoHandler;
        private readonly AutoMocker _mocker;
        private readonly PacienteTestsAutoMockerFixture _pacientefixture;
        public AdicionarAgendamentoCommandHandlerTests(PacienteTestsAutoMockerFixture pacientefixture)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AgendamentoProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(mapperConfig.CreateMapper());
            _agendamentoHandler = _mocker.CreateInstance<AdicionarAgendamentoCommandHandler>();
            _pacientefixture = pacientefixture;
        }

        [Fact(DisplayName = "Adicionar agendamento Novo Agendamento com Sucesso")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task AdicionarAgendamento_NovoAgendamento_DeveExecutarComSucesso()
        {
            var medico = new Medico(Guid.NewGuid(), "PR-1234567", null, "Dr. João", "64545337007", "86370000", "Rua 15 de março", "São Paulo", "43999999999", "jose@gmail.com");

            var especialidade = new Especialidade("Pediatra", "Atendimento de segunda a sexta");

            var especialidadeMedico = new EspecialidadeMedico(especialidade.Id, medico.Id, DateTime.Parse("2023-05-09 09:00:00"));

            var agendaMedico = new AgendaMedico(especialidadeMedico.MedicoId, DayOfWeek.Wednesday, TimeSpan.Parse("09:00"), TimeSpan.Parse("18:00"));

            var paciente = _pacientefixture.GerarPacienteValido();

            var agendamento = new Agendamento(especialidadeMedico.Id, paciente.Id, DateTime.Parse("2023-05-09 09:00:00"));
            //Arrange
            var agendamentoCommand = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2024-10-09 09:00:00"),
                EspecialidadeMedicoId = especialidadeMedico.Id,
                PacienteId = paciente.Id,
            };

            //Agenda Medico
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<AgendaMedico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.ObterAgendaMedicoPorDia(agendaMedico.MedicoId, (int)agendaMedico.DiaSemana)).ReturnsAsync(agendaMedico);

            //Especialidade
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.Adicionar(It.IsAny<Especialidade>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeRepository>().Setup(r => r.ObterPorId(especialidade.Id)).ReturnsAsync(especialidade);

            //Agendamento
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.Adicionar(It.IsAny<Agendamento>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.ObterPorId(agendamento.Id)).ReturnsAsync(agendamento);

            //Medico
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<Medico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.ObterPorId(especialidadeMedico.MedicoId)).ReturnsAsync(medico);

            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<EspecialidadeMedico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.ObterPorId(especialidadeMedico.Id)).ReturnsAsync(especialidadeMedico);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.VerificarAgendaLivreMedico(especialidadeMedico.MedicoId, agendamentoCommand.DataHoraAtendimento)).ReturnsAsync(true);

            _mocker.GetMock<IPacienteRepository>().Setup(r => r.Adicionar(It.IsAny<Paciente>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.ObterPorId(paciente.Id)).ReturnsAsync(paciente);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.VerificarPacienteCadastrado(paciente.Id)).ReturnsAsync(true);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.VerificarAgendaLivrePaciente(paciente.Id, agendamentoCommand.DataHoraAtendimento)).ReturnsAsync(true);
            


            var validator = new AdicionarAgendamentoCommandValidator();
            _mocker.GetMock<IAgendamentoRepository>().Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None);
            var validationResult = await validator.ValidateAsync(agendamentoCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Agendamento>()), Times.Once);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Agendamento Novo Agendamento com Falha")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task AdicionarAgendamento_NovoAgendamento_DeveExecutarComFalha()
        {
            //Arrange
            var agendamentoCommand = new AdicionarAgendamentoCommand { 
                DataHoraAtendimento = DateTime.Today,
                EspecialidadeMedicoId = Guid.Empty,
                PacienteId = Guid.Empty
            };
            var validator = new AdicionarAgendamentoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);

            //Assert
            Assert.False(validationResult.IsValid);
            await Assert.ThrowsAsync<BadRequestException>(() => _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None));
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Agendamento>()), Times.Never);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Adicionar Agendamento Data Indisponivel")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task AdicionarAgendamento_DataIndisponivel_DeveExecutarComFalha()
        {
            //Arrange
            var agendamentoCommand = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2024-05-04"), //Sabado
                EspecialidadeMedicoId = Guid.NewGuid(),
                PacienteId = Guid.NewGuid(),
            };
            var validator = new AdicionarAgendamentoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            await Assert.ThrowsAsync<BadRequestException>(() => _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None));
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Agendamento>()), Times.Never);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Adicionar Agendamento Horario Limitado")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task AdicionarAgendamento_DataPassada_DeveExecutarComFalha()
        {
            //Arrange
            var agendamentoCommand = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2024-05-03 09:00:00"), 
                EspecialidadeMedicoId = Guid.NewGuid(),
                PacienteId = Guid.NewGuid(),
            };
            var validator = new AdicionarAgendamentoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            await Assert.ThrowsAsync<BadRequestException>(() => _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None));
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Agendamento>()), Times.Never);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Adicionar Agendamento Horario Nao permitido")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task AdicionarAgendamento_HorarioIndisponivel_DeveExecutarComFalha()
        {
            //Arrange
            var agendamentoCommand = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2031-07-09 08:00:00"),
                EspecialidadeMedicoId = Guid.NewGuid(),
                PacienteId = Guid.NewGuid(),
            };
            var validator = new AdicionarAgendamentoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            await Assert.ThrowsAsync<BadRequestException>(() => _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None));
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Agendamento>()), Times.Never);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Adicionar Agendamento Horario Medico Indisponivel")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task AdicionarAgendamento_HorarioMedicoIndisponivel_DeveExecutarComFalha()
        {
            //Arrange
            var medico = new Medico(Guid.NewGuid(), "PR-1234567",null, "Dr. João", "64545337007", "86370000", "Rua 15 de março", "São Paulo", "43999999999", "jose@gmail.com");
            var especialidadeMedico = new EspecialidadeMedico(Guid.NewGuid(), medico.Id, DateTime.Parse("2023-06-05 09:00:00"));
            var agendaMedico = new AgendaMedico(medico.Id, DayOfWeek.Tuesday, TimeSpan.Parse("09:00"), TimeSpan.Parse("18:00"));
            var agendamento = new Agendamento(especialidadeMedico.Id, Guid.NewGuid(), DateTime.Parse("2029-06-05 09:00:00"));
            
            var agendamentoCommand = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2029-06-05 09:00:00"),
                EspecialidadeMedicoId = especialidadeMedico.Id,
                PacienteId = Guid.NewGuid(),
            };

            //Agendamento
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.Adicionar(It.IsAny<Agendamento>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.ObterPorId(agendamento.Id)).ReturnsAsync(agendamento);

            //Medico
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<Medico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IMedicoRepository>().Setup(r => r.ObterPorId(medico.Id)).ReturnsAsync(medico);

            //Agenda Medico
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<AgendaMedico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.ObterAgendaMedicoPorDia(agendaMedico.MedicoId, (int) agendaMedico.DiaSemana)).ReturnsAsync(agendaMedico);

            //EspecilidadeMedico
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<EspecialidadeMedico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.ObterPorId(especialidadeMedico.Id)).ReturnsAsync(especialidadeMedico);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.VerificarAgendaLivreMedico(especialidadeMedico.MedicoId, agendamentoCommand.DataHoraAtendimento)).ReturnsAsync(false);

            var validator = new AdicionarAgendamentoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);

            //Assert
            var excecao = await Assert.ThrowsAsync<BadRequestException>(() => _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None));
            Assert.True(validationResult.IsValid);
            Assert.Equal("Esse médico não poderá te atender nesse horário!", excecao.Message);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Agendamento>()), Times.Never);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Adicionar Agendamento Paciente Não Localizado")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task AdicionarAgendamento_PacienteNaoLocalizado_DeveExecutarComFalha()
        {
            //Arrange
            var especialidadeMedico = new EspecialidadeMedico(Guid.NewGuid(), Guid.NewGuid(), DateTime.Parse("2023-06-05 09:00:00"));
            var paciente = _pacientefixture.GerarPacienteValido();
            var agendamentoCommand = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2029-06-05 09:00:00"),
                EspecialidadeMedicoId = especialidadeMedico.Id,
                PacienteId = Guid.NewGuid(),
            };          

            //EspecilidadeMedico
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<EspecialidadeMedico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.ObterPorId(especialidadeMedico.Id)).ReturnsAsync(especialidadeMedico);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.VerificarAgendaLivreMedico(especialidadeMedico.MedicoId, agendamentoCommand.DataHoraAtendimento)).ReturnsAsync(true);

            //EspecilidadeMedico
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.Adicionar(It.IsAny<Paciente>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.VerificarPacienteCadastrado(paciente.Id)).ReturnsAsync(true);           

            var validator = new AdicionarAgendamentoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            await Assert.ThrowsAsync<BadRequestException>(() => _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None));
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Agendamento>()), Times.Never);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Adicionar Agendamento Horario Fracionado")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task AdicionarAgendamento_HorarioFracionado_DeveExecutarComFalha()
        {
            //Arrange
            var agendamentoCommand = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2031-07-09 09:27:00"),
                EspecialidadeMedicoId = Guid.NewGuid(),
                PacienteId = Guid.NewGuid(),
            };
            var validator = new AdicionarAgendamentoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            await Assert.ThrowsAsync<BadRequestException>(() => _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None));
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Agendamento>()), Times.Never);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Adicionar Agendamento Especialidade Medico Nao Localizada")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task AdicionarAgendamento_EspecialidadeMedicoNaoLocalizada_DeveExecutarComFalha()
        {
            //Arrange
            var agendamentoCommand = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2031-07-09 10:00:00"),
                EspecialidadeMedicoId = Guid.NewGuid(),
                PacienteId = Guid.NewGuid(),
            };
            var validator = new AdicionarAgendamentoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            await Assert.ThrowsAsync<BadRequestException>(() => _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None));
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Agendamento>()), Times.Never);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Adicionar Agendamento Horario Paciente Indisponivel")]
        [Trait("Categoria", "Agendamento - Agendamento Command Handler")]
        public async Task AdicionarAgendamento_HorarioPacienteIndisponivel_DeveExecutarComFalha()
        {
            //Arrange
            var especialidadeMedico = new EspecialidadeMedico(Guid.NewGuid(), Guid.NewGuid(), DateTime.Parse("2023-06-05 09:00:00"));
            var especialidadeMedico2 = new EspecialidadeMedico(Guid.NewGuid(), Guid.NewGuid(), DateTime.Parse("2023-06-05 09:00:00"));
            var paciente = _pacientefixture.GerarPacienteValido();
            var agendamento = new Agendamento(especialidadeMedico.Id, paciente.Id, DateTime.Parse("2029-06-05 09:00:00"));
            var agendaMedico = new AgendaMedico(especialidadeMedico2.MedicoId, DayOfWeek.Tuesday, TimeSpan.Parse("09:00"), TimeSpan.Parse("18:00"));

            var agendamentoCommand = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2029-06-05 09:00:00"),
                EspecialidadeMedicoId = especialidadeMedico2.Id,
                PacienteId = paciente.Id,
            };

            //Agendamento
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.Adicionar(It.IsAny<Agendamento>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendamentoRepository>().Setup(r => r.ObterPorId(agendamento.Id)).ReturnsAsync(agendamento);

            //Agenda Medico
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<AgendaMedico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IAgendaMedicoRepository>().Setup(r => r.ObterAgendaMedicoPorDia(agendaMedico.MedicoId, (int)agendaMedico.DiaSemana)).ReturnsAsync(agendaMedico);

            //EspecilidadeMedico
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.Adicionar(It.IsAny<EspecialidadeMedico>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.ObterPorId(especialidadeMedico.Id)).ReturnsAsync(especialidadeMedico);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.VerificarAgendaLivreMedico(especialidadeMedico.MedicoId, agendamentoCommand.DataHoraAtendimento)).ReturnsAsync(true);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.ObterPorId(especialidadeMedico2.Id)).ReturnsAsync(especialidadeMedico2);
            _mocker.GetMock<IEspecialidadeMedicoRepository>().Setup(r => r.VerificarAgendaLivreMedico(especialidadeMedico2.MedicoId, agendamentoCommand.DataHoraAtendimento)).ReturnsAsync(true);
           
            //Paciente
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.Adicionar(It.IsAny<Paciente>())).Returns(Task.CompletedTask);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.ObterPorId(paciente.Id)).ReturnsAsync(paciente);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.VerificarPacienteCadastrado(paciente.Id)).ReturnsAsync(true);
            _mocker.GetMock<IPacienteRepository>().Setup(r => r.VerificarAgendaLivrePaciente(paciente.Id, agendamentoCommand.DataHoraAtendimento)).ReturnsAsync(false);

            var validator = new AdicionarAgendamentoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(agendamentoCommand);

            //Assert
            var excecao = await Assert.ThrowsAsync<BadRequestException>(() => _agendamentoHandler.Handle(agendamentoCommand, CancellationToken.None));
            Assert.True(validationResult.IsValid);
            Assert.Equal("Esse paciente não pode ser atendimento nesse horário!", excecao.Message);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Agendamento>()), Times.Never);
            _mocker.GetMock<IAgendamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }
    }
}

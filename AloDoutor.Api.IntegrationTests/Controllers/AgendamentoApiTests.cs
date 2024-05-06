using AloDoutor.Api.IntegrationTests.Config;
using AloDoutor.Application.Features.Agendamentos.Commands.AdicionarAgendamento;
using AloDoutor.Application.Features.Especialidades.Commands.AdicionarEspecialidade;
using AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico;
using AloDoutor.Application.Features.Pacientes.Commands.AdicionarPaciente;
using AloDoutor.Application.ViewModel;
using System.Net.Http.Json;
using Xunit;

namespace AloDoutor.Api.IntegrationTests.Controllers
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AgendamentoApiTests
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;

        public AgendamentoApiTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar Novo Paciente")]
        [Trait("Categoria", "Integração API - Paciente")]
        public async Task Adicionar_novoPaciente_DeveRetornarComSucesso()
        {
            var paciente = _testsFixture.GerarPacienteCommand(1).FirstOrDefault();

            // Envia requisição para criar o paciente
            var postResponsePaciente = await _testsFixture.Client.PostAsJsonAsync("Paciente", paciente);
            postResponsePaciente.EnsureSuccessStatusCode();

            // Extrai o ID do paciente criado da resposta
            var pacienteCriado = await postResponsePaciente.Content.ReadFromJsonAsync<IdViewModel>();
            var pacienteId = pacienteCriado;

            var especialidade = _testsFixture.GerarEspecialidadeCommand(1).FirstOrDefault();

            // Envia requisição para criar o especialidade
            var postResponseEspecialidade = await _testsFixture.Client.PostAsJsonAsync("api/Especialidade", especialidade);
            postResponseEspecialidade.EnsureSuccessStatusCode();

            // Extrai o ID do especialidade criado da resposta
            var especialdiadeCriado = await postResponseEspecialidade.Content.ReadFromJsonAsync<IdViewModel>();
            var especialidadeId = especialdiadeCriado.Id;

            var medico = _testsFixture.GerarMedicoCommand(1).FirstOrDefault();

            // Envia requisição para criar o Medico
            var postResponseMedico = await _testsFixture.Client.PostAsJsonAsync("Medico", medico);
            postResponseMedico.EnsureSuccessStatusCode();

            // Extrai o ID do medico criado da resposta
            var medicoCriado = await postResponseMedico.Content.ReadFromJsonAsync<IdViewModel>();
            var medicoId = medicoCriado;

            var especialidadeMedico = new AdicionarEspecialidadeMedicoCommand
            {
                DataRegistro = DateTime.Parse("2024-05-06"),
                EspecialidadeId = especialidadeId,
                MedicoId = medicoId.Id               
            };

            var postResponseEspecialidadeMedico = await _testsFixture.Client.PostAsJsonAsync("api/especialidade-medico", especialidadeMedico);
            postResponseEspecialidadeMedico.EnsureSuccessStatusCode();

            // Extrai o ID do medico criado da resposta
            var especialidadeMedicoCriado = await postResponseEspecialidadeMedico.Content.ReadFromJsonAsync<IdViewModel>();
            var especialidadeMedicoId = especialidadeMedicoCriado;

            var agendamento = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2029-11-29 09:00:00"),
                EspecialidadeMedicoId = especialidadeMedicoId.Id,
                PacienteId = pacienteId.Id
            };

            //Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/Cadastrar", agendamento);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Adicionar Novo Agendamento")]
        [Trait("Categoria", "Integração API - Agendamento")]
        public async Task Adicionar_novoAgendamento_DeveRetornarComSucesso()
        {
            var agendamento = new AdicionarAgendamentoCommand
            {
                DataHoraAtendimento = DateTime.Parse("2029-11-29 09:00:00"),
                EspecialidadeMedicoId = Guid.Parse("C07B5E55-27A0-402E-A6FB-0CBAC1E54D25"),
                PacienteId = Guid.Parse("EB42BAC9-2A54-4AA9-B72E-0D6B31262268"),
            };

            //Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/Cadastrar", agendamento);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }

   
}

using AloDoutor.Application.Features.Especialidades.Commands.AdicionarEspecialidade;
using AloDoutor.Application.Features.Medicos.Commands.AdicionarMedico;
using AloDoutor.Application.Features.Pacientes.Commands.AdicionarPaciente;
using AloDoutor.Domain.Entity;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AloDoutor.Api.IntegrationTests.Config
{
    [CollectionDefinition(nameof(IntegrationWebTestsFixtureCollection))]
    public class IntegrationWebTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>> { }

    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>> { }
    public class IntegrationTestsFixture<TProgram> : IDisposable where TProgram : class
    {
        public string AntiForgeryFieldName = "__RequestVerificationToken";


        public readonly Factory<TProgram> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };

            Factory = new Factory<TProgram>();
            Client = Factory.CreateClient(clientOptions);
        }

        public IEnumerable<Paciente> GerarPaciente(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var pacientes = new Faker<Paciente>("pt_BR")
                .CustomInstantiator(f => new Paciente(
                    Guid.NewGuid(),
                    f.Random.Number(1, 100).ToString(),
                    null,
                    f.Name.FirstName() + " " + f.Name.LastName(),
                    f.Person.Cpf(false),
                    f.Address.ZipCode(),
                    f.Address.StreetName(),
                    f.Address.State(),
                    f.Phone.PhoneNumber()
                ));

            return pacientes.Generate(quantidade);
        }

        public IEnumerable<AdicionarPacienteCommand> GerarPacienteCommand(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var pacientes = new Faker<AdicionarPacienteCommand>("pt_BR")
                .CustomInstantiator(f => new AdicionarPacienteCommand(
                    f.Name.FirstName() + " " + f.Name.LastName(),
                    f.Person.Cpf(false),
                    f.Address.ZipCode().Replace("-", ""),
                    f.Address.StreetName(),
                    f.Address.State(),
                    f.Random.Number(1, 100).ToString(),
                    f.Phone.PhoneNumber().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("+", "").Substring(2)
                ));

            return pacientes.Generate(quantidade);
        }

        public IEnumerable<AdicionarMedicoCommand> GerarMedicoCommand(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var medicos = new Faker<AdicionarMedicoCommand>("pt_BR")
                .CustomInstantiator(f => new AdicionarMedicoCommand(
                    f.Name.FirstName() + " " + f.Name.LastName(),
                    f.Person.Cpf(false),
                    f.Address.ZipCode().Replace("-", ""),
                    f.Address.StreetName(),
                    f.Address.State(),
                    f.Address.StateAbbr() + "-" + f.Random.Number(10000, 99999),
                    f.Phone.PhoneNumber().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("+", "").Substring(2)
                ));

            return medicos.Generate(quantidade);
        }

        public IEnumerable<AdicionarEspecialidadeCommand> GerarEspecialidadeCommand(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var medicos = new Faker<AdicionarEspecialidadeCommand>("pt_BR")
                .CustomInstantiator(f => new AdicionarEspecialidadeCommand(
                   f.Random.Word(),
                   "Atendimento de segunda a sexta"
                ));

            return medicos.Generate(quantidade);
        }


        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}

using HealthMedScheduler.Application.Features.Pacientes.Commands.AdicionarPaciente;
using HealthMedScheduler.Application.Features.Pacientes.Commands.AtualizarPaciente;
using HealthMedScheduler.Domain.Entity;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;

namespace HealthMedScheduler.Application.UnitTests.Mocks
{
    [CollectionDefinition(nameof(PacienteTestsAutoMockerCollection))]
    public class PacienteTestsAutoMockerCollection : ICollectionFixture<PacienteTestsAutoMockerFixture>
    {
    }
    public class PacienteTestsAutoMockerFixture : IDisposable
    {
        public AdicionarPacienteCommand GerarPacienteCommandValido()
        {
            return GerarPacienteCommand(1).FirstOrDefault();
        }

        public AdicionarPacienteCommand GerarPacienteCommandInValido()
        {
            return GerarPacienteCommandVazio(1).FirstOrDefault();
        }

        public Paciente GerarPacienteValido()
        {
            return GerarPaciente(1).FirstOrDefault();
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
                    f.Phone.PhoneNumber(),
                    f.Internet.Email()
                ));

            return pacientes.Generate(quantidade);
        }

        public AtualizarPacienteCommand AtualizarPacienteCommand()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var pacientes = new Faker<AtualizarPacienteCommand>("pt_BR")
                .CustomInstantiator(f => new AtualizarPacienteCommand(
                    Guid.NewGuid(),
                    f.Name.FirstName() + " " + f.Name.LastName(),
                    f.Person.Cpf(false),
                    f.Address.ZipCode(),
                    f.Address.StreetName(),
                    f.Address.State(),
                    f.Random.Number(1, 100).ToString(),
                    f.Phone.PhoneNumber()
                ));

            return pacientes;
        }

        public IEnumerable<AdicionarPacienteCommand> GerarPacienteCommand(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var pacientes = new Faker<AdicionarPacienteCommand>("pt_BR")
                .CustomInstantiator(f => new AdicionarPacienteCommand(
                    f.Name.FirstName() + " " + f.Name.LastName(),
                    f.Person.Cpf(false),
                    f.Address.ZipCode(),
                    f.Address.StreetName(),
                    f.Address.State(),
                    f.Random.Number(1, 100).ToString(),
                    f.Phone.PhoneNumber(),
                    f.Internet.Email(),
                    "Teste@123",
                    "Teste@123"
                ));

            return pacientes.Generate(quantidade);
        }

        public IEnumerable<AdicionarPacienteCommand> GerarPacienteCommandVazio(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var pacientes = new Faker<AdicionarPacienteCommand>("pt_BR")
                .CustomInstantiator(f => new AdicionarPacienteCommand(
                    "",
                    "",
                    "",
                    "",
                    "",
                     "",
                    "",
                    "",
                     "",
                    ""
                ));

            return pacientes.Generate(quantidade);

        }
        public void Dispose()
        {
           
        }
    }
}

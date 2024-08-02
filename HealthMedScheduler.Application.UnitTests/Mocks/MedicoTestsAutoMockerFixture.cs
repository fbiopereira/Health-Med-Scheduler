using HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarMedico;
using HealthMedScheduler.Application.Features.Medicos.Commands.AtualizarMedico;
using HealthMedScheduler.Domain.Entity;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;

namespace HealthMedScheduler.Application.UnitTests.Mocks
{
    [CollectionDefinition(nameof(MedicoTestsAutoMockerCollection))]
    public class MedicoTestsAutoMockerCollection : ICollectionFixture<MedicoTestsAutoMockerFixture>
    {
    }
    public class MedicoTestsAutoMockerFixture : IDisposable
    {
        public AdicionarMedicoCommand GerarMedicoCommandValido()
        {
            return GerarMedicoCommand(1).FirstOrDefault();
        }

        public AdicionarMedicoCommand GerarMedicoCommandInValido()
        {
            return GerarMedicoCommandVazio(1).FirstOrDefault();
        }

        public Medico GerarMedicoValido()
        {
            return GerarMedico(1).FirstOrDefault();
        }

        public IEnumerable<Medico> GerarMedico(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var medicos = new Faker<Medico>("pt_BR")
                .CustomInstantiator(f => new Medico(
                    Guid.NewGuid(),
                     f.Address.StateAbbr() + "-" + f.Random.Number(10000, 99999),
                     null,
                    f.Name.FirstName() + " " + f.Name.LastName(),
                    f.Person.Cpf(false),
                    f.Address.ZipCode(),
                    f.Address.StreetName(),
                    f.Address.State(),
                    f.Phone.PhoneNumber(),
                    f.Internet.Email()
                ));

            return medicos.Generate(quantidade);
        }

        public AtualizarMedicoCommand AtualizarMedicoCommand()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var medicos = new Faker<AtualizarMedicoCommand>("pt_BR")
                .CustomInstantiator(f => new AtualizarMedicoCommand(
                    Guid.NewGuid(),
                    f.Name.FirstName() + " " + f.Name.LastName(),
                    f.Person.Cpf(false),
                    f.Address.ZipCode(),
                    f.Address.StreetName(),
                    f.Address.State(),
                     f.Address.StateAbbr() + "-" + f.Random.Number(10000, 99999),
                    f.Phone.PhoneNumber()
                ));

            return medicos;
        }

        public IEnumerable<AdicionarMedicoCommand> GerarMedicoCommand(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var medicos = new Faker<AdicionarMedicoCommand>("pt_BR")
                .CustomInstantiator(f => new AdicionarMedicoCommand(
                    f.Name.FirstName() + " " + f.Name.LastName(),
                    f.Person.Cpf(false),
                    f.Address.ZipCode(),
                    f.Address.StreetName(),
                    f.Address.State(),
                     f.Address.StateAbbr() + "-" + f.Random.Number(10000, 99999),
                    f.Phone.PhoneNumber(),
                    f.Internet.Email(),
                    "Teste@123",
                    "Teste@123"
                ));

            return medicos.Generate(quantidade);
        }

        public IEnumerable<AdicionarMedicoCommand> GerarMedicoCommandVazio(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var medicos = new Faker<AdicionarMedicoCommand>("pt_BR")
                .CustomInstantiator(f => new AdicionarMedicoCommand(
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

            return medicos.Generate(quantidade);

        }
        public void Dispose()
        {
           
        }
    }
}

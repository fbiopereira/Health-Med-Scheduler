using AloDoutor.Application.Features.Medicos.Commands.AdicionarMedico;
using AloDoutor.Application.Interfaces;
using AloDoutor.Domain.Entity;
using AutoMapper;
using Moq;

namespace AloDoutor.Application.UnitTests.Mocks
{
    public class MockMedicoRepository
    {
        public static Mock<IMedicoRepository> ObterTodosMockMedicoRepository()
        {
            var listaMedicos = new List<Medico>
            {
                new Medico("123456", null, "Medico 1", "11111111111", "2222-2222", "Rua Fake", "Fake", "777777777"),
                new Medico("654321", null, "Medico 2", "22222222222", "3333-3333", "Rua Mock", "Mock", "888888888"),
                new Medico("565654", null, "Medico 3", "33333333333", "4444-4444", "Rua Test", "Test", "999999999")            
            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var mockMedicoRepo = new Mock<IMedicoRepository>();
            mockMedicoRepo.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);
            //Mock para obter todos os medicos
            mockMedicoRepo.Setup(x => x.ObterTodos()).ReturnsAsync(listaMedicos);

            //Mock para adicionar um medico
            mockMedicoRepo.Setup(r => r.Adicionar(It.IsAny<Medico>()))
               .Returns((Medico medico) =>
               {
                   listaMedicos.Add(medico);
                   return unitOfWorkMock.Object.Commit();
               });
            return mockMedicoRepo;
        }
    }
}

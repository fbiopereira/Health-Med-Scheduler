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
                new Medico(Guid.Parse("11111111-1111-1111-1111-111111111111"), "123456", null, "Medico 1", "11111111111", "2222-2222", "Rua Fake", "Fake", "777777777"),
                new Medico(Guid.Parse("22222222-2222-2222-2222-222222222222"), "654321", null, "Medico 2", "22222222222", "3333-3333", "Rua Mock", "Mock", "888888888"),
                new Medico(Guid.Parse("33333333-3333-3333-3333-333333333333"),"565654", null, "Medico 3", "33333333333", "4444-4444", "Rua Test", "Test", "999999999")            
            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var mockMedicoRepo = new Mock<IMedicoRepository>();
            mockMedicoRepo.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);

            //Mock para obter todos os medicos
            mockMedicoRepo.Setup(x => x.ObterTodos()).ReturnsAsync(listaMedicos);

            //Mock para obter um medico por id
            mockMedicoRepo.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => listaMedicos.FirstOrDefault(x => x.Id == id));

            //Mock para adicionar um medico
            mockMedicoRepo.Setup(r => r.Adicionar(It.IsAny<Medico>()))
               .Returns((Medico medico) =>
               {
                   listaMedicos.Add(medico);
                   return unitOfWorkMock.Object.Commit();
               });

            //Mock para atualizar um medico
            mockMedicoRepo.Setup(r => r.Atualizar(It.IsAny<Medico>()))
               .Returns((Medico medico) =>
               {
                   var index = listaMedicos.FindIndex(x => x.Id == medico.Id);
                   if (index >= 0)
                   {
                       listaMedicos[index] = medico;
                   }
                   return unitOfWorkMock.Object.Commit();
               });

            //Mock para remover um medico
            mockMedicoRepo.Setup(r => r.Remover(It.IsAny<Medico>()))
               .Returns((Medico medico) =>
               {
                   var index = listaMedicos.FindIndex(x => x.Id == medico.Id);
                   if (index >= 0)
                   {
                       listaMedicos.RemoveAt(index);
                   }
                   return unitOfWorkMock.Object.Commit();
               });

            return mockMedicoRepo;
        }
    }
}

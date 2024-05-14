using AloDoutor.Application.Features.Pacientes.Commands.AdicionarPaciente;
using AloDoutor.Application.Features.Pacientes.Commands.AtualizarPaciente;
using AloDoutor.Application.ViewModel;
using AloDoutor.Domain.Entity;
using AutoMapper;

namespace AloDoutor.Application.MappingProfiles
{
    public class PacienteProfile : Profile
    {
        public PacienteProfile()
        {
            //Escrita
            CreateMap<AdicionarPacienteCommand, Paciente>();
            CreateMap<AtualizarPacienteCommand, Paciente>();

            //Leitura
            CreateMap<Paciente, PacienteViewModel>()
                 .ForMember(dest => dest.agendasPaciente, opt => opt.MapFrom(src => src.Agendamentos));
        }

    }
}

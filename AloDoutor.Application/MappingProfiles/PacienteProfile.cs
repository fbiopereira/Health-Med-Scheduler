using AloDoutor.Application.DTO;
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
            CreateMap<PacienteDTO, Paciente>();

            //Recuperar lista de agendamentos para do Paciente
            CreateMap<Paciente, PacienteViewModel>()
                .ForMember(dest => dest.agendasPaciente, opt => opt.MapFrom(src => src.Agendamentos));
        }

    }
}

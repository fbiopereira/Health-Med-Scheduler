using AloDoutor.Application.Features.Medicos.Commands.AdicionarMedico;
using AloDoutor.Application.Features.Medicos.Commands.AtualizarMedico;
using AloDoutor.Application.ViewModel;
using AloDoutor.Domain.Entity;
using AutoMapper;

namespace AloDoutor.Application.MappingProfiles
{
    public class MedicoProfile : Profile
    {
        public MedicoProfile()
        {
            //Escrita
            CreateMap<AdicionarMedicoCommand, Medico>();
            CreateMap<AtualizarMedicoCommand, Medico>();


            //Leitura
            CreateMap<Medico, MedicoViewModel>();
            CreateMap<Medico, MedicoViewModel>()
                .ForMember(dest => dest.Especialidades, opt => opt.MapFrom(src => src.EspecialidadesMedicos))
                .ForMember(dest => dest.agendasMedico, opt => opt.MapFrom(src => src.EspecialidadesMedicos.SelectMany(e => e.Agendamentos ?? Enumerable.Empty<Agendamento>())));

        }
    }
}

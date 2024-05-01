using AloDoutor.Application.Features.Especialidades.Commands.AdicionarEspecialidade;
using AloDoutor.Application.Features.Especialidades.Commands.AtualizarEspecialidade;
using AloDoutor.Application.ViewModel;
using AloDoutor.Domain.Entity;
using AutoMapper;

namespace AloDoutor.Application.MappingProfiles
{
    public class EspecialidadeProfile : Profile
    {
        public EspecialidadeProfile()
        {
            //Escrita
            CreateMap<AdicionarEspecialidadeCommand, Especialidade>();
            CreateMap<AtualizarEspecialidadeCommand, Especialidade>();

            //Leitura
            CreateMap<Especialidade, EspecialidadeViewModel>()
               .ForMember(dest => dest.Medicos, opt => opt.MapFrom(src => src.EspecialidadeMedicos));
          
        }
    }
}

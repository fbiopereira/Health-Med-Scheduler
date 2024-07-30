using HealthMedScheduler.Application.Features.Especialidades.Commands.AdicionarEspecialidade;
using HealthMedScheduler.Application.Features.Especialidades.Commands.AtualizarEspecialidade;
using HealthMedScheduler.Application.ViewModel;
using HealthMedScheduler.Domain.Entity;
using AutoMapper;

namespace HealthMedScheduler.Application.MappingProfiles
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

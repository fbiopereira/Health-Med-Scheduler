using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico;
using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AtualizarEspecialidadeMedico;
using HealthMedScheduler.Application.ViewModel;
using HealthMedScheduler.Domain.Entity;
using AutoMapper;

namespace HealthMedScheduler.Application.MappingProfiles
{
    public class EspecialidadeMedicoProfile : Profile
    {
        public EspecialidadeMedicoProfile()
        {
            //Escrita
            CreateMap<AdicionarEspecialidadeMedicoCommand, EspecialidadeMedico>();
            CreateMap<AtualizarEspecialidadeMedicoCommand, EspecialidadeMedico>();


            //Leitura
            CreateMap<EspecialidadeMedico, EspecialidadeMedicosViewModel>();

        }
    }
}

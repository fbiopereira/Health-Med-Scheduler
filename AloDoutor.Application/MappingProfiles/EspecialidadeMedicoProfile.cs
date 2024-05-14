using AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico;
using AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AtualizarEspecialidadeMedico;
using AloDoutor.Application.ViewModel;
using AloDoutor.Domain.Entity;
using AutoMapper;

namespace AloDoutor.Application.MappingProfiles
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

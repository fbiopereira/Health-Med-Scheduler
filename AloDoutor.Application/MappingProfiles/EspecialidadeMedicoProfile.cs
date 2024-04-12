using AloDoutor.Application.DTO;
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
            CreateMap<EspecialidadeMedicoDTO, EspecialidadeMedico>();

            //Leitura
            CreateMap<EspecialidadeMedico, EspecialidadeMedicosViewModel>();


        }
    }
}

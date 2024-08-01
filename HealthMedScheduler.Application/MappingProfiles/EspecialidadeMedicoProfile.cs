using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico;
using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AtualizarEspecialidadeMedico;
using HealthMedScheduler.Application.ViewModel;
using HealthMedScheduler.Domain.Entity;
using AutoMapper;
using HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarAgenda;

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

            CreateMap<AdicionarAgendaMedicoCommand, AgendaMedico>()
                .ConstructUsing(src => new AgendaMedico(
                   src.MedicoId,
                  (DayOfWeek) src.DiaSemana,
                   TimeSpan.Parse(src.HoraInicio),
                   TimeSpan.Parse(src.HoraFim)
                ));
        }    
    }
}
 
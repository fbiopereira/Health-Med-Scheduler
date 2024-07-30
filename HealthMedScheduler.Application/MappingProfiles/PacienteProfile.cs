using HealthMedScheduler.Application.Features.Pacientes.Commands.AdicionarPaciente;
using HealthMedScheduler.Application.Features.Pacientes.Commands.AtualizarPaciente;
using HealthMedScheduler.Application.ViewModel;
using HealthMedScheduler.Domain.Entity;
using AutoMapper;

namespace HealthMedScheduler.Application.MappingProfiles
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

using HealthMedScheduler.Application.Features.Agendamentos.Commands.AdicionarAgendamento;
using HealthMedScheduler.Application.ViewModel;
using HealthMedScheduler.Domain.Entity;
using AutoMapper;

namespace HealthMedScheduler.Application.MappingProfiles
{
    public class AgendamentoProfile : Profile
    {
        public AgendamentoProfile()
        {
            // Escrita
            CreateMap<AdicionarAgendamentoCommand, Agendamento>();

            //Leitura
            CreateMap<Agendamento, AgendamentoViewModel>()
                .ForMember(dest => dest.NomeEspecialidade, opt => opt.MapFrom(src => src.EspecialidadeMedico.Especialidade.Nome))
                .ForMember(dest => dest.NomePaciente, opt => opt.MapFrom(src => src.Paciente.Nome))
                .ForMember(dest => dest.CpfPaciente, opt => opt.MapFrom(src => src.Paciente.Cpf))
                .ForMember(dest => dest.NomeMedico, opt => opt.MapFrom(src => src.EspecialidadeMedico.Medico.Nome))
                .ForMember(dest => dest.CrmMedico, opt => opt.MapFrom(src => src.EspecialidadeMedico.Medico.Crm));

            CreateMap<Agendamento, AgendamentoMedicoViewModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.NomePaciente, opt => opt.MapFrom(src => src.Paciente.Nome))
               .ForMember(dest => dest.NomeEspecialidade, opt => opt.MapFrom(src => src.EspecialidadeMedico.Especialidade.Nome))
               .ForMember(dest => dest.DataHoraAtendimento, opt => opt.MapFrom(src => src.DataHoraAtendimento))
               .ForMember(dest => dest.StatusAgendamento, opt => opt.MapFrom(src => src.StatusAgendamento));

            CreateMap<Agendamento, AgendamentoPacienteViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NomeMedico, opt => opt.MapFrom(src => src.EspecialidadeMedico.Medico.Nome))
                .ForMember(dest => dest.NomeEspecialidade, opt => opt.MapFrom(src => src.EspecialidadeMedico.Especialidade.Nome))
                .ForMember(dest => dest.StatusAgendamento, opt => opt.MapFrom(src => src.StatusAgendamento))
                .ForMember(dest => dest.DataHoraAtendimento, opt => opt.MapFrom(src => src.DataHoraAtendimento));
        }
    }
}

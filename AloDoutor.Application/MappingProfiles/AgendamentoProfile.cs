using AloDoutor.Application.DTO;
using AloDoutor.Application.ViewModel;
using AloDoutor.Domain.Entity;
using AutoMapper;

namespace AloDoutor.Application.MappingProfiles
{
    public class AgendamentoProfile : Profile
    {
        public AgendamentoProfile()
        {
            CreateMap<AgendamentoDTO, Agendamento>();

            //Configurar agendamentoPaciente
            CreateMap<Agendamento, AgendamentoPacienteViewModel>()
                .ForMember(dest => dest.NomeEspecialidade, opt => opt.MapFrom(src => src.EspecialidadeMedico.Especialidade.Nome))
                .ForMember(dest => dest.NomeMedico, opt => opt.MapFrom(src => src.EspecialidadeMedico.Medico.Nome));

            //Configurar agendamentoMedico
            CreateMap<Agendamento, AgendamentoMedicoViewModel>()
                .ForMember(dest => dest.NomeEspecialidade, opt => opt.MapFrom(src => src.EspecialidadeMedico.Especialidade.Nome))
                .ForMember(dest => dest.NomePaciente, opt => opt.MapFrom(src => src.Paciente.Nome));

            CreateMap<Agendamento, AgendamentoViewModel>()
                .ForMember(dest => dest.NomeEspecialidade, opt => opt.MapFrom(src => src.EspecialidadeMedico.Especialidade.Nome))
                .ForMember(dest => dest.NomePaciente, opt => opt.MapFrom(src => src.Paciente.Nome))
                .ForMember(dest => dest.CpfPaciente, opt => opt.MapFrom(src => src.Paciente.Cpf))
                .ForMember(dest => dest.NomeMedico, opt => opt.MapFrom(src => src.EspecialidadeMedico.Medico.Nome))
                .ForMember(dest => dest.CrmMedico, opt => opt.MapFrom(src => src.EspecialidadeMedico.Medico.Crm));
        }
    }
}

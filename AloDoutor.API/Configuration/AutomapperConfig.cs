using AloDoutor.Api.Application.DTO;
using AloDoutor.Api.Application.ViewModel;
using AloDoutor.Domain.Entity;
using AutoMapper;

namespace AloDoutor.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            //Escrita
            CreateMap<AgendamentoDTO, Agendamento>();
            CreateMap<PacienteDTO, Paciente>();
            CreateMap<MedicoDTO, Medico>();
            CreateMap<EspecialidadeDTO, Especialidade>();
            CreateMap<EspecialidadeMedicoDTO, EspecialidadeMedico>();

            //Leitura
            CreateMap<Medico, EspecialidadeMedicosViewModel>();
            CreateMap<Especialidade, MedicoEspecialidadesViewModel>();

            //Obter todas as especialidades de um médico
            CreateMap<Medico, MedicoViewModel>()
                .ForMember(dest => dest.Especialidades, opt => opt.MapFrom(src => src.EspecialidadesMedicos.Select(e => e.Especialidade)))
                .ForMember(dest => dest.agendasMedico, opt => opt.MapFrom(src => src.EspecialidadesMedicos.SelectMany(a => a.Agendamentos ?? Enumerable.Empty<Agendamento>()))); 

            //Obter todos os medicos de uma especialidade
            CreateMap<Especialidade, EspecialidadeViewModel>()
                .ForMember(dest => dest.Medicos, opt => opt.MapFrom(src => src.EspecialidadeMedicos.Select(m => m.Medico)));

            //Recuperar lista de agendamentos para do Paciente
            CreateMap<Paciente, PacienteViewModel>()
                .ForMember(dest => dest.agendasPaciente, opt => opt.MapFrom(src => src.Agendamentos));

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

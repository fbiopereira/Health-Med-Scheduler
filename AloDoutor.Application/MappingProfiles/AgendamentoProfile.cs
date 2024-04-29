using AloDoutor.Application.Features.Agendamentos.Commands.AdicionarAgendamento;
using AloDoutor.Application.Features.Especialidades.Commands.AdicionarEspecialidade;
using AloDoutor.Application.Features.Especialidades.Commands.AtualizarEspecialidade;
using AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico;
using AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AtualizarEspecialidadeMedico;
using AloDoutor.Application.Features.Medicos.Commands.AdicionarMedico;
using AloDoutor.Application.Features.Medicos.Commands.AtualizarMedico;
using AloDoutor.Application.Features.Pacientes.Commands.AdicionarPaciente;
using AloDoutor.Application.Features.Pacientes.Commands.AtualizarPaciente;
using AloDoutor.Application.ViewModel;
using AloDoutor.Domain.Entity;
using AutoMapper;

namespace AloDoutor.Application.MappingProfiles
{
    public class AgendamentoProfile : Profile
    {
        public AgendamentoProfile()
        {
            // CreateMap<AgendamentoDTO, Agendamento>();
            CreateMap<AdicionarAgendamentoCommand, Agendamento>();
            CreateMap<AdicionarEspecialidadeCommand, Especialidade>();
            CreateMap<AtualizarEspecialidadeCommand, Especialidade>();
            CreateMap<AdicionarMedicoCommand, Medico>();
            CreateMap<AtualizarMedicoCommand, Medico>();
            CreateMap<AdicionarPacienteCommand, Paciente>();
            CreateMap<AtualizarPacienteCommand, Paciente>();
            CreateMap<AdicionarEspecialidadeMedicoCommand, EspecialidadeMedico>();
            CreateMap<AtualizarEspecialidadeMedicoCommand, EspecialidadeMedico>();

            //Leitura
            CreateMap<Agendamento, AgendamentoViewModel>()
                .ForMember(dest => dest.NomeEspecialidade, opt => opt.MapFrom(src => src.EspecialidadeMedico.Especialidade.Nome))
                .ForMember(dest => dest.NomePaciente, opt => opt.MapFrom(src => src.Paciente.Nome))
                .ForMember(dest => dest.CpfPaciente, opt => opt.MapFrom(src => src.Paciente.Cpf))
                .ForMember(dest => dest.NomeMedico, opt => opt.MapFrom(src => src.EspecialidadeMedico.Medico.Nome))
                .ForMember(dest => dest.CrmMedico, opt => opt.MapFrom(src => src.EspecialidadeMedico.Medico.Crm));


            CreateMap<Especialidade, EspecialidadeViewModel>()
                .ForMember(dest => dest.Medicos, opt => opt.MapFrom(src => src.EspecialidadeMedicos));

            CreateMap<EspecialidadeMedico, EspecialidadeMedicosViewModel>();
            CreateMap<Medico, MedicoViewModel>();

            CreateMap<Medico, MedicoViewModel>()
                .ForMember(dest => dest.Especialidades, opt => opt.MapFrom(src => src.EspecialidadesMedicos))
                .ForMember(dest => dest.agendasMedico, opt => opt.MapFrom(src => src.EspecialidadesMedicos.SelectMany(e => e.Agendamentos ?? Enumerable.Empty<Agendamento>())));

            CreateMap<Agendamento, AgendamentoMedicoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NomePaciente, opt => opt.MapFrom(src => src.Paciente.Nome))
                .ForMember(dest => dest.NomeEspecialidade, opt => opt.MapFrom(src => src.EspecialidadeMedico.Especialidade.Nome))
                .ForMember(dest => dest.DataHoraAtendimento, opt => opt.MapFrom(src => src.DataHoraAtendimento))
                .ForMember(dest => dest.StatusAgendamento, opt => opt.MapFrom(src => src.StatusAgendamento));

            CreateMap<Paciente, PacienteViewModel>()
                .ForMember(dest => dest.agendasPaciente, opt => opt.MapFrom(src => src.Agendamentos));

            CreateMap<Agendamento, AgendamentoPacienteViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NomeMedico, opt => opt.MapFrom(src => src.EspecialidadeMedico.Medico.Nome))
                .ForMember(dest => dest.NomeEspecialidade, opt => opt.MapFrom(src => src.EspecialidadeMedico.Especialidade.Nome))
                .ForMember(dest => dest.StatusAgendamento, opt => opt.MapFrom(src => src.StatusAgendamento))
                .ForMember(dest => dest.DataHoraAtendimento, opt => opt.MapFrom(src => src.DataHoraAtendimento));
            /*
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
            */
        }
    }
}

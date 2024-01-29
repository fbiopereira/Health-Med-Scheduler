using AloDoutor.Api.Application.DTO;
using AloDoutor.Api.Application.ViewModel;
using AloDoutor.Core.Controllers;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AloDoutor.Api.Controllers
{
    //[Authorize]
    [Route("Medico")]
    public class MedicoController :  MainController<MedicoController>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMedicoService _medicoService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public MedicoController(IMedicoRepository medicoRepository, IMapper mapper, IMedicoService medicoService, ILogger<MedicoController> logger) : base(logger)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
            _medicoService = medicoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os médicos cadastrados.
        /// </summary>
        /// <returns>Uma lista de médicos.</returns>
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            _logger.LogInformation("Endpoint de obtenção de todos médicos cadastrados.");
            return CustomResponse(_mapper.Map<IEnumerable<MedicoViewModel>>(await _medicoRepository.ObterTodos()));
        }

        /// <summary>
        /// Obtém um médico por ID.
        /// </summary>
        /// <param name="id">O ID do médico a ser obtido.</param>
        /// <returns>O médico encontrado ou um erro 404 se não encontrado.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            _logger.LogInformation("Endpoint de obtenção de médico por ID.");
            return CustomResponse(_mapper.Map<MedicoViewModel>(await _medicoRepository.ObterPorId(id)));
        }

        /// <summary>
        /// Obtém as especialidades de um médico por ID do médico.
        /// </summary>
        /// <param name="idMedico">O ID do médico para obter as especialidades.</param>
        /// <returns>O médico com suas especialidades ou um erro 404 se não encontrado.</returns>
        [HttpGet("MedicoEspecialidades/{idMedico:guid}")]
        public async Task<ActionResult> ObterMedicoEspecialidadePorIdMedico(Guid idMedico)
        {
            _logger.LogInformation("Endpoint para obtenção de especialidades do médico por ID do médico.");
            return CustomResponse(_mapper.Map<MedicoViewModel>(await _medicoRepository.ObterEspecialidadesPorIdMedico(idMedico)));
        }

        /// <summary>
        /// Obtém os agendamentos de um médico por ID do médico.
        /// </summary>
        /// <param name="idMedico">O ID do médico para obter os agendamentos.</param>
        /// <returns>O médico com seus agendamentos ou um erro 404 se não encontrado.</returns>
        [HttpGet("Agendamento/{idMedico:guid}")]
        public async Task<ActionResult> ObterAgendamentoPorMedico(Guid idMedico)
        {
            _logger.LogInformation("Endpoint para obtenção de agendamentos por médico.");
            return CustomResponse(_mapper.Map<MedicoViewModel>(await _medicoRepository.ObterAgendamentosPorIdMedico(idMedico)));
        }

        /// <summary>
        /// Adiciona um novo médico.
        /// </summary>
        /// <param name="medicoDTO">Os dados do médico a ser adicionado.</param>
        /// <returns>O médico adicionado ou um erro 400 em caso de falha na adição.</returns>
        [HttpPost]
        public async Task<ActionResult> Adicionar(MedicoDTO medicoDTO)
        {
            //verificar o custom response para created
            _logger.LogInformation("Endpoint para cadastramento de medico.");
            var validation = await _medicoService.Adicionar(_mapper.Map<Medico>(medicoDTO));
           return validation.IsValid ?  Created("", medicoDTO) :  CustomResponse(validation);         
        }

        /// <summary>
        /// Atualiza um médico existente.
        /// </summary>
        /// <param name="medicoDTO">Os novos dados do médico a ser atualizado.</param>
        /// <returns>Um código 201 em caso de sucesso na atualização ou um erro 400 em caso de falha.</returns>
        [HttpPut()]
        public async Task<ActionResult> Atualizar(MedicoDTO medicoDTO)
        {
            _logger.LogInformation("Endpoint para alteração de cadastro do médico.");
            return CustomResponse(await _medicoService.Atualizar(_mapper.Map<Medico>(medicoDTO)));
        }

        /// <summary>
        /// Remove um médico por ID.
        /// </summary>
        /// <param name="id">O ID do médico a ser removido.</param>
        /// <returns>Um código 201 em caso de sucesso na remoção ou um erro 404 se não encontrado.</returns>
        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
            _logger.LogInformation("Endpoint para excluir cadastro do médico.");
            return CustomResponse(await _medicoService.Remover(id));
        }
    }
}

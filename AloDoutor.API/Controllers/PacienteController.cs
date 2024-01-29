using AloDoutor.Api.Application.DTO;
using AloDoutor.Api.Application.ViewModel;
using AloDoutor.Core.Controllers;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AloDoutor.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AloDoutor.Api.Controllers
{
    [Authorize]
    [Route("Paciente")]
    public class PacienteController : MainController<PacienteController>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPacienteService _pacienteService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PacienteController(IPacienteRepository pascienteRepository, IPacienteService pacienteService, IMapper mapper,
            ILogger<PacienteController> logger) : base(logger)
        {
            _pacienteRepository = pascienteRepository;
            _pacienteService = pacienteService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os pacientes cadastrados.
        /// </summary>
        /// <returns>Uma lista de pacientes.</returns>
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            _logger.LogInformation("Endpoint de obtenção de todos pacientes cadastrados.");
            return CustomResponse(await _pacienteRepository.ObterTodos());
        }

        /// <summary>
        /// Obtém um paciente por ID.
        /// </summary>
        /// <param name="id">O ID do paciente a ser obtido.</param>
        /// <returns>O paciente encontrado ou um erro 404 se não encontrado.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            _logger.LogInformation("Endpoint de obtenção de paciente por ID.");
            return CustomResponse(await _pacienteRepository.ObterPorId(id));
        }

        /// <summary>
        /// Obtém os agendamentos de um paciente por ID do paciente.
        /// </summary>
        /// <param name="idPaciente">O ID do paciente para obter os agendamentos.</param>
        /// <returns>O paciente com seus agendamentos ou um erro 404 se não encontrado.</returns>
        [HttpGet("Agendamento/{idPaciente:guid}")]
        public async Task<ActionResult> ObterAgendamentoPorPaciente(Guid idPaciente)
        {
            _logger.LogInformation("Endpoint para obtenção de agendamentos por paciente.");
            return CustomResponse(_mapper.Map<PacienteViewModel>(await _pacienteRepository.ObterAgendamentosPorIdPaciente(idPaciente)));
        }

        /// <summary>
        /// Adiciona um novo paciente.
        /// </summary>
        /// <param name="pacienteDTO">Os dados do paciente a ser adicionado.</param>
        /// <returns>O paciente adicionado ou um erro 400 em caso de falha na adição.</returns>
        [HttpPost]
        public async Task<ActionResult> Adicionar(PacienteDTO pacienteDTO)
        {
            _logger.LogInformation("Endpoint para cadastramento de paciente.");

            var validation = await _pacienteService.Adicionar(_mapper.Map<Paciente>(pacienteDTO));
            return validation.IsValid ? Created("", pacienteDTO) : CustomResponse(validation);
        }

        /// <summary>
        /// Atualiza um paciente existente.
        /// </summary>
        /// <param name="pacienteDTO">Os novos dados do paciente a ser atualizado.</param>
        /// <returns>Um código 201 em caso de sucesso na atualização ou um erro 400 em caso de falha.</returns>
        [HttpPut]
        public async Task<ActionResult> Atualizar(PacienteDTO pacienteDTO)
        {
            _logger.LogInformation("Endpoint para alteração de cadastro do paciente.");
            return CustomResponse(await _pacienteService.Atualizar(_mapper.Map<Paciente>(pacienteDTO)));
        }

        /// <summary>
        /// Remove um paciente por ID.
        /// </summary>
        /// <param name="id">O ID do paciente a ser removido.</param>
        /// <returns>Um código 201 em caso de sucesso na remoção ou um erro 404 se não encontrado.</returns>
        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
            _logger.LogInformation("Endpoint para excluir cadastro do paciente.");
            return CustomResponse(await _pacienteService.Remover(id));
        }
    }
}

using AloDoutor.Api.Application.DTO;
using AloDoutor.Api.Application.ViewModel;
using AloDoutor.Core.Controllers;
using AloDoutor.Core.Usuario;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace AloDoutor.Api.Controllers
{
    [Authorize]
    [Route("api")]
    public class AgendamentoController : MainController<AgendamentoController>
    {
        private readonly IAspNetUser _user;
        private readonly IAgendamentoService _agendamentoService;
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AgendamentoController(IAgendamentoService agendamentoService, IMapper mapper, 
            IAgendamentoRepository agendamentoRepository, ILogger<AgendamentoController> logger, IAspNetUser user) : base(logger)
        {
            _agendamentoService = agendamentoService;
            _mapper = mapper;
            _agendamentoRepository = agendamentoRepository;
            _logger = logger;
            _user = user;
        }

        /// <summary>
        /// Obtém todos os agendamentos cadastrados.
        /// </summary>
        /// <returns>Uma lista de agendamentos.</returns>
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            _logger.LogInformation("Endpoint de obtenção de todos agendamentos cadastrados.");
            return CustomResponse(await _agendamentoRepository.ObterTodos());
        }

        /// <summary>
        /// Obtém um agendamento por ID.
        /// </summary>
        /// <param name="id">O ID do agendamento a ser obtido.</param>
        /// <returns>O agendamento encontrado ou um erro 404 se não encontrado.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            _logger.LogInformation("Endpoint de obtenção de agendamento por ID.");
            return CustomResponse(await _agendamentoRepository.ObterPorId(id));
        }

        /// <summary>
        /// Obtém agendamentos por status.
        /// </summary>
        /// <param name="status">O status dos agendamentos a serem obtidos.</param>
        /// <returns>Uma lista de agendamentos com o status especificado.</returns>
        [HttpGet("Por-status/{status:int}")]
        public async Task<ActionResult> ObterAgendamentoPorStatus(int status)
        {
            _logger.LogInformation("Endpoint de obtenção de agendamentos por status.");
            return CustomResponse(_mapper.Map<IEnumerable<AgendamentoViewModel>>(await _agendamentoRepository.ObterAgendamentosPorIStatus(status)));
        }

        /// <summary>
        /// Adiciona um novo agendamento.
        /// </summary>
        /// <param name="agendamentoDTO">Os dados do agendamento a ser adicionado.</param>
        /// <returns>O agendamento adicionado ou um erro 400 em caso de falha na adição.</returns>
        [HttpPost("Cadastrar")]
        public async Task<ActionResult> Adicionar(AgendamentoDTO agendamentoDTO)
        {
            _logger.LogInformation("Endpoint para cadastramento de agendamento.");
            var validation = await _agendamentoService.Adicionar(_mapper.Map<Agendamento>(agendamentoDTO));            
            return validation.IsValid ? Created("", agendamentoDTO) : CustomResponse(validation);
        }

        /// <summary>
        /// Realiza um reagendamento.
        /// </summary>
        /// <param name="id">O ID do agendamento a ser reagendado.</param>
        /// <param name="data">A nova data de agendamento.</param>
        /// <returns>Um código 201 em caso de sucesso no reagendamento ou um erro 400 em caso de falha.</returns>
        [HttpPut("Reagendar/{id:guid}/{data:datetime}")]
        public async Task<ActionResult> Cancelar(Guid id, DateTime data)
        {
            _logger.LogInformation("Endpoint para realizar um reagendamento.");
            return CustomResponse(await _agendamentoService.Reagendar(id, data));
        }

        /// <summary>
        /// Cancela um agendamento por ID.
        /// </summary>
        /// <param name="id">O ID do agendamento a ser cancelado.</param>
        /// <returns>Um código 201 em caso de sucesso no cancelamento ou um erro 400 em caso de falha.</returns>
        [HttpPut("Cancelar")]
        public async Task<ActionResult> Cancelar(Guid id)
        {
            _logger.LogInformation("Endpoint para cancelar o agendamento por ID.");
            return CustomResponse(await _agendamentoService.Cancelar(id));
        }
    }
}

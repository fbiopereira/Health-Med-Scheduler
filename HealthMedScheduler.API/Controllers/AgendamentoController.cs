using HealthMedScheduler.Application.Features.Agendamentos.Commands.AdicionarAgendamento;
using HealthMedScheduler.Application.Features.Agendamentos.Commands.AtualizarAgendamento;
using HealthMedScheduler.Application.Features.Agendamentos.Commands.RemoverAgendamento;
using HealthMedScheduler.Application.Features.Agendamentos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealthMedScheduler.Api.Controllers
{
    [Route("api")]
    public class AgendamentoController : MainController<AgendamentoController>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        public AgendamentoController(ILogger<AgendamentoController> logger, IMediator mediator) : base(logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém todos os agendamentos cadastrados.
        /// </summary>
        /// <returns>Uma lista de agendamentos.</returns>
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var agendamentos = await _mediator.Send(new ObterAgendamentoQuery());
            return CustomResponse(agendamentos);
        }

        /// <summary>
        /// Obtém um agendamento por ID.
        /// </summary>
        /// <param name="id">O ID do agendamento a ser obtido.</param>
        /// <returns>O agendamento encontrado ou um erro 404 se não encontrado.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            var agendamento = await _mediator.Send(new ObterAgendamentoPorIdQuery(id));
            return CustomResponse(agendamento);
        }

        /// <summary>
        /// Obtém agendamentos por status.
        /// </summary>
        /// <param name="status">O status dos agendamentos a serem obtidos.</param>
        /// <returns>Uma lista de agendamentos com o status especificado.</returns>
        [HttpGet("Por-status/{status:int}")]
        public async Task<ActionResult> ObterAgendamentoPorStatus(int status)
        {
            var agendamentos = await _mediator.Send(new ObterAgendamentoPorStatusQuery(status));
            return CustomResponse(agendamentos);
        }

        /// <summary>
        /// Adiciona um novo agendamento.
        /// </summary>
        /// <param name="agendamentoDTO">Os dados do agendamento a ser adicionado.</param>
        /// <returns>O agendamento adicionado ou um erro 400 em caso de falha na adição.</returns>
        [HttpPost("Cadastrar")]
        public async Task<ActionResult> Adicionar(AdicionarAgendamentoCommand agendamento)
        {
            _logger.LogInformation("Endpoint para cadastramento de agendamento.");
            var response = await _mediator.Send(agendamento);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }

        /// <summary>
        /// Realiza um reagendamento.
        /// </summary>
        /// <param name="id">O ID do agendamento a ser reagendado.</param>
        /// <param name="data">A nova data de agendamento.</param>
        /// <returns>Um código 201 em caso de sucesso no reagendamento ou um erro 400 em caso de falha.</returns>
        [HttpPut("Reagendar/{id:guid}/{data:datetime}")]
        public async Task<ActionResult> Reagendar(RealizarReagendamentoCommand agendamento)
        {
            _logger.LogInformation("Endpoint para realizar um reagendamento.");
            var response = await _mediator.Send(agendamento);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }

        /// <summary>
        /// Cancela um agendamento por ID.
        /// </summary>
        /// <param name="id">O ID do agendamento a ser cancelado.</param>
        /// <returns>Um código 201 em caso de sucesso no cancelamento ou um erro 400 em caso de falha.</returns>
        [HttpPut("Cancelar")]
        public async Task<ActionResult> Cancelar(RemoverAgendamentoCommand agendamento)
        {
            _logger.LogInformation("Endpoint para cancelar o agendamento por ID.");
            var response = await _mediator.Send(agendamento);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }
    }
}

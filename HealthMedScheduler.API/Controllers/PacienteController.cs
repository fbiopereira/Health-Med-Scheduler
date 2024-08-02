using Health.Core.Controllers;
using HealthMedScheduler.Application.Features.Pacientes.Commands.AdicionarPaciente;
using HealthMedScheduler.Application.Features.Pacientes.Commands.AtualizarPaciente;
using HealthMedScheduler.Application.Features.Pacientes.Commands.RemoverPaciente;
using HealthMedScheduler.Application.Features.Pacientes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMedScheduler.Api.Controllers
{
    [Authorize]
    [Route("Paciente")]
    public class PacienteController : MainController<PacienteController>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public PacienteController(
            ILogger<PacienteController> logger, IMediator mediator) : base(logger)
        {

            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém todos os pacientes cadastrados.
        /// </summary>
        /// <returns>Uma lista de pacientes.</returns>
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var medicos = await _mediator.Send(new ObterPacientesQuery());
            return CustomResponse(medicos);
        }

        /// <summary>
        /// Obtém um paciente por ID.
        /// </summary>
        /// <param name="id">O ID do paciente a ser obtido.</param>
        /// <returns>O paciente encontrado ou um erro 404 se não encontrado.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            var medicos = await _mediator.Send(new ObterPacientePorIdQuery(id));
            return CustomResponse(medicos);
        }
        
        /// <summary>
        /// Obtém os agendamentos de um paciente por ID do paciente.
        /// </summary>
        /// <param name="idPaciente">O ID do paciente para obter os agendamentos.</param>
        /// <returns>O paciente com seus agendamentos ou um erro 404 se não encontrado.</returns>
        [HttpGet("Agendamento/{idPaciente:guid}")]
        public async Task<ActionResult> ObterAgendamentoPorPaciente(Guid idPaciente)
        {
            var medicos = await _mediator.Send(new ObterAgendamentoPacientePorIdQuery(idPaciente));
            return CustomResponse(medicos);
        }

        /// <summary>
        /// Adiciona um novo paciente.
        /// </summary>
        /// <param name="pacienteDTO">Os dados do paciente a ser adicionado.</param>
        /// <returns>O paciente adicionado ou um erro 400 em caso de falha na adição.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Adicionar(AdicionarPacienteCommand paciente)
        {
            _logger.LogInformation("Endpoint para cadastramento de paciente.");

            var response = await _mediator.Send(paciente);
            //return validation.IsValid ? Created("", medicoDTO) : CustomResponse(validation);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }
        
        /// <summary>
        /// Atualiza um paciente existente.
        /// </summary>
        /// <param name="pacienteDTO">Os novos dados do paciente a ser atualizado.</param>
        /// <returns>Um código 201 em caso de sucesso na atualização ou um erro 400 em caso de falha.</returns>
        [HttpPut]
        public async Task<ActionResult> Atualizar(AtualizarPacienteCommand paciente)
        {
            _logger.LogInformation("Endpoint para alteração de cadastro do paciente.");
            var response = await _mediator.Send(paciente);
            //return validation.IsValid ? Created("", medicoDTO) : CustomResponse(validation);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }

        /// <summary>
        /// Remove um paciente por ID.
        /// </summary>
        /// <param name="id">O ID do paciente a ser removido.</param>
        /// <returns>Um código 201 em caso de sucesso na remoção ou um erro 404 se não encontrado.</returns>
        [HttpDelete]
        public async Task<ActionResult> Remover(RemoverPacienteCommand paciente)
        {
            var response = await _mediator.Send(paciente);
            _logger.LogInformation("Endpoint para remoção de cadastro do médico.");
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }
    }
}

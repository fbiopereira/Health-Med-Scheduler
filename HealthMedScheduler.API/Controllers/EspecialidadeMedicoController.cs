using Health.Core.Controllers;
using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico;
using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AtualizarEspecialidadeMedico;
using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.RemoverEspecialidadeMedico;
using HealthMedScheduler.Application.Features.EspecialidadesMedicos.Queries;
using HealthMedScheduler.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMedScheduler.Api.Controllers
{
    [Authorize]
    [Route("api/especialidade-medico")]
    public class EspecialidadeMedicoController : MainController<EspecialidadeMedicoController>
    {

        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public EspecialidadeMedicoController(ILogger<EspecialidadeMedicoController> logger, IMediator mediator) : base(logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém todas as especialidades de médico cadastradas.
        /// </summary>
        /// <returns>Uma lista de especialidades de médico.</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EspecialidadeMedico>))]
        public async Task<IActionResult> ObterTodos()
        {
            var especialidadesMedicos = await _mediator.Send(new ObterEspecialidadeMedicoQuery());
            return CustomResponse(especialidadesMedicos);

        }

        /// <summary>
        /// Obtém uma especialidade de médico por ID.
        /// </summary>
        /// <param name="id">O ID da especialidade de médico a ser obtida.</param>
        /// <returns>A especialidade de médico encontrada ou um erro 404 se não encontrada.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(EspecialidadeMedico))]
        [ProducesResponseType(404)]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            var medicos = await _mediator.Send(new ObterMedicoEspecialidadePorIdQuery(id));
            return CustomResponse(medicos);
        }
        /// <summary>
        /// Adiciona uma nova especialidade de médico.
        /// </summary>
        /// <param name="especialidadeDTO">Os dados da especialidade de médico a ser adicionada.</param>
        /// <returns>A especialidade de médico adicionada ou um erro 400 em caso de falha na adição.</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(EspecialidadeMedico))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Adicionar(AdicionarEspecialidadeMedicoCommand especialidadeMedico)
        {
            _logger.LogInformation("Endpoint para cadastramento de especialidades do medico.");
            var response = await _mediator.Send(especialidadeMedico);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }

        /// <summary>
        /// Atualiza uma especialidade de médico existente.
        /// </summary>
        /// <param name="especialidadeDTO">Os novos dados da especialidade de médico a ser atualizada.</param>
        /// <returns>Um código 201 em caso de sucesso na atualização ou um erro 400 em caso de falha.</returns>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Atualizar(AtualizarEspecialidadeMedicoCommand especialidadeMedico)
        {
            _logger.LogInformation("Endpoint para alteração de cadastro da especialidades de médico.");
            var response = await _mediator.Send(especialidadeMedico);
            //return validation.IsValid ? Created("", medicoDTO) : CustomResponse(validation);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }

        /// <summary>
        /// Remove uma especialidade de médico por ID.
        /// </summary>
        /// <param name="id">O ID da especialidade de médico a ser removida.</param>
        /// <returns>Um código 201 em caso de sucesso na remoção ou um erro 404 se não encontrada.</returns>
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Remover(RemoverEspecialidadeMedicoCommand especialidadeMedico)
        {
            _logger.LogInformation("Endpoint para excluir cadastro de especialidades de médico.");
            var response = await _mediator.Send(especialidadeMedico);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }
    }
}

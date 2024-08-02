using Health.Core.Controllers;
using HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarAgenda;
using HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarMedico;
using HealthMedScheduler.Application.Features.Medicos.Commands.AtualizarMedico;
using HealthMedScheduler.Application.Features.Medicos.Commands.RemoverMedico;
using HealthMedScheduler.Application.Features.Medicos.Queries.ObterTodosMedicos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMedScheduler.Api.Controllers
{
    [Authorize]
    [Route("Medico")]
    public class MedicoController : MainController<MedicoController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public MedicoController(IMediator mediator, ILogger<MedicoController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os médicos cadastrados.
        /// </summary>
        /// <returns>Uma lista de médicos.</returns>
        [HttpGet]
        public async Task<ActionResult> ObterTodos()
        {
            var medicos = await _mediator.Send(new ObterMedicosQuery());
            return CustomResponse(medicos);
        }

        /// <summary>
        /// Obtém um médico por ID.
        /// </summary>
        /// <param name="id">O ID do médico a ser obtido.</param>
        /// <returns>O médico encontrado ou um erro 404 se não encontrado.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            var medico = await _mediator.Send(new ObterMedicoPorIdQuery(id));
            return CustomResponse(medico);
        }

        /// <summary>
        /// Obtém as especialidades de um médico por ID do médico.
        /// </summary>
        /// <param name="idMedico">O ID do médico para obter as especialidades.</param>
        /// <returns>O médico com suas especialidades ou um erro 404 se não encontrado.</returns>
        [HttpGet("MedicoEspecialidades/{idMedico:guid}")]
        public async Task<ActionResult> ObterMedicoEspecialidadePorIdMedico(Guid idMedico)
        {
            var medico = await _mediator.Send(new ObterEspecialidadePorIdMedicoQuery(idMedico));
            return CustomResponse(medico);

        }

        /// <summary>
        /// Obtém os agendamentos de um médico por ID do médico.
        /// </summary>
        /// <param name="idMedico">O ID do médico para obter os agendamentos.</param>
        /// <returns>O médico com seus agendamentos ou um erro 404 se não encontrado.</returns>
        [HttpGet("Agendamento/{idMedico:guid}")]
        public async Task<ActionResult> ObterAgendamentoPorMedico(Guid idMedico)
        {
            var medico = await _mediator.Send(new ObterAgendamentoMedicoPorIdMedicoQuery(idMedico));
            return CustomResponse(medico);
        }

        [HttpPost("Cadastrar-Agenda")]
        public async Task<ActionResult> CadastrarAgendaMedico(AdicionarAgendaMedicoCommand agendaMedico)
        {
            var medico = await _mediator.Send(agendaMedico);
            return CustomResponse(medico);
        }

        /// <summary>
        /// Adiciona um novo médico.
        /// </summary>
        /// <param name="medicoDTO">Os dados do médico a ser adicionado.</param>
        /// <returns>O médico adicionado ou um erro 400 em caso de falha na adição.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Adicionar(AdicionarMedicoCommand medico)
        {

            //_logger.LogInformation("Endpoint para cadastramento de medico.");
            var response = await _mediator.Send(medico);
            //return validation.IsValid ? Created("", medicoDTO) : CustomResponse(validation);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }

        /// <summary>
        /// Atualiza um médico existente.
        /// </summary>
        /// <param name="medicoDTO">Os novos dados do médico a ser atualizado.</param>
        /// <returns>Um código 201 em caso de sucesso na atualização ou um erro 400 em caso de falha.</returns>
        [HttpPut()]
        public async Task<ActionResult> Atualizar(AtualizarMedicoCommand medico)
        {
            var response = await _mediator.Send(medico);
            _logger.LogInformation("Endpoint para alteração de cadastro do médico.");
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }

        /// <summary>
        /// Remove um médico por ID.
        /// </summary>
        /// <param name="id">O ID do médico a ser removido.</param>
        /// <returns>Um código 201 em caso de sucesso na remoção ou um erro 404 se não encontrado.</returns>
        [HttpDelete]
        public async Task<ActionResult> Remover(RemoverMedicoCommand medico)
        {
            var response = await _mediator.Send(medico);
            _logger.LogInformation("Endpoint para remoção de cadastro do médico.");
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }
    }
}

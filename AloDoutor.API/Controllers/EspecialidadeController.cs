using AloDoutor.Application.Features.Especialidades.Commands.AdicionarEspecialidade;
using AloDoutor.Application.Features.Especialidades.Commands.AtualizarEspecialidade;
using AloDoutor.Application.Features.Especialidades.Commands.RemoverEspecialidade;
using AloDoutor.Application.Features.Especialidades.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AloDoutor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController : MainController<EspecialidadeController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public EspecialidadeController(IMediator mediator, ILogger<EspecialidadeController> logger): base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

         /// <summary>
         /// Obtém todas as especialidades cadastradas.
         /// </summary>
         /// <returns>Uma lista de especialidades.</returns>
         [HttpGet]
         public async Task<IActionResult> ObterTodos()
         {
            var especialidadesMedicos = await _mediator.Send(new ObterEspecialidadeQuery());
            return CustomResponse(especialidadesMedicos);
        }
        
         /// <summary>
         /// Obtém uma especialidade por ID.
         /// </summary>
         /// <param name="id">O ID da especialidade a ser obtida.</param>
         /// <returns>A especialidade encontrada ou um erro 404 se não encontrada.</returns>
         [HttpGet("{id:guid}")]
         public async Task<ActionResult> ObterPorId(Guid id)
         {
            var especialidade = await _mediator.Send(new ObterEspecialidadePorIdQuery(id));
            return CustomResponse(especialidade);
        }

         /// <summary>
         /// Obtém médicos com uma especialidade por ID de especialidade.
         /// </summary>
         /// <param name="idEspecialidade">O ID da especialidade para obter os médicos.</param>
         /// <returns>A especialidade com os médicos associados ou um erro 404 se não encontrada.</returns>
         [HttpGet("EspecialidadeMedicos/{idEspecialidade:guid}")]
         public async Task<ActionResult> ObterMedicoEspecialidadePorIdMedico(Guid idEspecialidade)
         {
            var especialidade = await _mediator.Send(new ObterEspecialidadePorIdQuery(idEspecialidade));
            return CustomResponse(especialidade);
        }

        /// <summary>
        /// Adiciona uma nova especialidade.
        /// </summary>
        /// <param name="especialidade">Os dados da especialidade a ser adicionada.</param>
        /// <returns>A especialidade adicionada ou um erro 400 em caso de falha na adição.</returns>
        [HttpPost]
        public async Task<ActionResult> Adicionar(AdicionarEspecialidadeCommand especialidade)
        {
            _logger.LogInformation("Endpoint para cadastramento de especialidade.");             
            var response = await _mediator.Send(especialidade);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
           
        }

        /// <summary>
        /// Atualiza uma especialidade existente.
        /// </summary>
        /// <param name="especialidadeDTO">Os novos dados da especialidade a ser atualizada.</param>
        /// <returns>Um código 201 em caso de sucesso na atualização ou um erro 400 em caso de falha.</returns>
        [HttpPut]
        public async Task<ActionResult> Atualizar(AtualizarEspecialidadeCommand especialidade)
        {
            var response = await _mediator.Send(especialidade);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }

        /// <summary>
        /// Remove uma especialidade por ID.
        /// </summary>
        /// <param name="id">O ID da especialidade a ser removida.</param>
        /// <returns>Um código 201 em caso de sucesso na remoção ou um erro 404 se não encontrada.</returns>
        [HttpDelete]
        public async Task<ActionResult> Remover(RemoverEspecialidadeCommand especialidade)
        {
            _logger.LogInformation("Endpoint para excluir cadastro da especialidade.");
            var response = await _mediator.Send(especialidade);
            return CreatedAtAction(nameof(ObterTodos), new { id = response });
        }
    }
}

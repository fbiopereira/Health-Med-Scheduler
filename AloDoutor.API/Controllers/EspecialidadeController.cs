using AloDoutor.Application.Features.Especialidades.Commands.AdicionarEspecialidade;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AloDoutor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EspecialidadeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /* /// <summary>
         /// Obtém todas as especialidades cadastradas.
         /// </summary>
         /// <returns>Uma lista de especialidades.</returns>
         [HttpGet]
         public async Task<IActionResult> ObterTodos()
         {
             _logger.LogInformation("Endpoint de obtenção de todas especialidades cadastradas.");
             return CustomResponse(_mapper.Map<IEnumerable<EspecialidadeViewModel>>(await _especialidadeService.ObterTodos()));
         }

         /// <summary>
         /// Obtém uma especialidade por ID.
         /// </summary>
         /// <param name="id">O ID da especialidade a ser obtida.</param>
         /// <returns>A especialidade encontrada ou um erro 404 se não encontrada.</returns>
         [HttpGet("{id:guid}")]
         public async Task<ActionResult> ObterPorId(Guid id)
         {
             _logger.LogInformation("Endpoint de obtenção de especialidade por ID.");
             return CustomResponse(_mapper.Map<EspecialidadeViewModel>(await _especialidadeService.ObterPorId(id)));
         }

         /// <summary>
         /// Obtém médicos com uma especialidade por ID de especialidade.
         /// </summary>
         /// <param name="idEspecialidade">O ID da especialidade para obter os médicos.</param>
         /// <returns>A especialidade com os médicos associados ou um erro 404 se não encontrada.</returns>
         [HttpGet("EspecialidadeMedicos/{idEspecialidade:guid}")]
         public async Task<ActionResult> ObterMedicoEspecialidadePorIdMedico(Guid idEspecialidade)
         {
             _logger.LogInformation("Endpoint para obtenção de médicos com uma especialidade por ID de especialidade.");
             return CustomResponse(_mapper.Map<EspecialidadeViewModel>(await _especialidadeService.ObterMedicosPorEspecialidadeId(idEspecialidade)));
         }*/

        /// <summary>
        /// Adiciona uma nova especialidade.
        /// </summary>
        /// <param name="especialidade">Os dados da especialidade a ser adicionada.</param>
        /// <returns>A especialidade adicionada ou um erro 400 em caso de falha na adição.</returns>
        [HttpPost]
        public async Task<ActionResult> Adicionar(AdicionarEspecialidadeCommand especialidade)
        {
            /* _logger.LogInformation("Endpoint para cadastramento de especialidade.");

             var validation = await _especialidadeService.Adicionar(_mapper.Map<Especialidade>(especialidadeDTO));
             return validation.IsValid ? Created("", especialidadeDTO) : CustomResponse(validation);*/
            var response = await _mediator.Send(especialidade);
            return CreatedAtAction("Especialidade Criada", new { id = response });
        }

        /*/// <summary>
        /// Atualiza uma especialidade existente.
        /// </summary>
        /// <param name="especialidadeDTO">Os novos dados da especialidade a ser atualizada.</param>
        /// <returns>Um código 201 em caso de sucesso na atualização ou um erro 400 em caso de falha.</returns>
        [HttpPut]
        public async Task<ActionResult> Atualizar(EspecialidadeDTO especialidadeDTO)
        {
            _logger.LogInformation("Endpoint para alteração de cadastro da especialidade.");
            return CustomResponse(await _especialidadeService.Atualizar(_mapper.Map<Especialidade>(especialidadeDTO)));
        }

        /// <summary>
        /// Remove uma especialidade por ID.
        /// </summary>
        /// <param name="id">O ID da especialidade a ser removida.</param>
        /// <returns>Um código 201 em caso de sucesso na remoção ou um erro 404 se não encontrada.</returns>
        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
            _logger.LogInformation("Endpoint para excluir cadastro da especialidade.");
            return CustomResponse(await _especialidadeService.Remover(id));
        }*/
    }
}

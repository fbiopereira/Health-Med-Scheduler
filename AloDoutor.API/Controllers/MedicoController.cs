using AloDoutor.Application.Features.Medicos.Commands.AdicionarMedico;
using AloDoutor.Application.Features.Medicos.Commands.AtualizarMedico;
using AloDoutor.Application.Features.Medicos.Commands.DeletarMedico;
using AloDoutor.Application.Features.Medicos.Queries.ObterMedicoPorId;
using AloDoutor.Application.Features.Medicos.Queries.ObterTodosMedicos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AloDoutor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        //private readonly IMedicoService _medicoService;
        private readonly IMediator _mediator;
        public MedicoController(IMediator mediator)
        {
            /* _medicoRepository = medicoRepository;
             _mapper = mapper;
             _medicoService = medicoService;
             _logger = logger;*/
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém todos os médicos cadastrados.
        /// </summary>
        /// <returns>Uma lista de médicos.</returns>
        [HttpGet]
        public async Task<IEnumerable<MedicoDTO>> ObterTodos()
        {
            var medicos = await _mediator.Send(new ObterMedicosQuery());
            //return CustomResponse(medicos);
            return medicos;
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
            return Ok(medico);
        }

        /// <summary>
        /// Obtém as especialidades de um médico por ID do médico.
        /// </summary>
        /// <param name="idMedico">O ID do médico para obter as especialidades.</param>
        /// <returns>O médico com suas especialidades ou um erro 404 se não encontrado.</returns>
       /* [HttpGet("MedicoEspecialidades/{idMedico:guid}")]
        public async Task<ActionResult> ObterMedicoEspecialidadePorIdMedico(Guid idMedico)
        {
            _logger.LogInformation("Endpoint para obtenção de especialidades do médico por ID do médico.");
            return CustomResponse(_mapper.Map<MedicoViewModel>(await _medicoService.ObterEspecialidadesPorIdMedico(idMedico)));
        }*/

        /// <summary>
        /// Obtém os agendamentos de um médico por ID do médico.
        /// </summary>
        /// <param name="idMedico">O ID do médico para obter os agendamentos.</param>
        /// <returns>O médico com seus agendamentos ou um erro 404 se não encontrado.</returns>
     /*   [HttpGet("Agendamento/{idMedico:guid}")]
        public async Task<ActionResult> ObterAgendamentoPorMedico(Guid idMedico)
        {
            _logger.LogInformation("Endpoint para obtenção de agendamentos por médico.");
            return CustomResponse(_mapper.Map<MedicoViewModel>(await _medicoService.ObterAgendamentosPorIdMedico(idMedico)));
        }*/

        /// <summary>
        /// Adiciona um novo médico.
        /// </summary>
        /// <param name="medico">Os dados do médico a ser adicionado.</param>
        /// <returns>O médico adicionado ou um erro 400 em caso de falha na adição.</returns>
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
        /// <param name="medico">Os novos dados do médico a ser atualizado.</param>
        /// <returns>Um código 201 em caso de sucesso na atualização ou um erro 400 em caso de falha.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(AtualizarMedicoCommand medico)
        {
            //_logger.LogInformation("Endpoint para alteração de cadastro do médico.");
            await _mediator.Send(medico);
            return NoContent();
        }

        /// <summary>
        /// Remove um médico por ID.
        /// </summary>
        /// <param name="id">O ID do médico a ser removido.</param>
        /// <returns>Um código 201 em caso de sucesso na remoção ou um erro 404 se não encontrado.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var command = new DeletarMedicoCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

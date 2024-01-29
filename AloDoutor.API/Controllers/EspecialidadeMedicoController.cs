using AloDoutor.Api.Application.DTO;
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
    [Route("api/especialidade-medico")]
    public class EspecialidadeMedicoController : MainController<EspecialidadeMedicoController>
    {
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        private readonly IEspecialidadeMedicoService _especialidadeMedicoService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EspecialidadeMedicoController(IEspecialidadeMedicoRepository especialidadeMedicoRepository,
            IMapper mapper, IEspecialidadeMedicoService especialidadeMedicoService, ILogger<EspecialidadeMedicoController> logger) : base(logger)
        {
            _especialidadeMedicoRepository = especialidadeMedicoRepository;
            _mapper = mapper;
            _especialidadeMedicoService = especialidadeMedicoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todas as especialidades de médico cadastradas.
        /// </summary>
        /// <returns>Uma lista de especialidades de médico.</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EspecialidadeMedico>))]
        public async Task<IActionResult> ObterTodos()
        {
            _logger.LogInformation("Endpoint de obtenção de todas especialidades de médico cadastradas.");
            return CustomResponse(await _especialidadeMedicoRepository.ObterTodos());
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
            _logger.LogInformation("Endpoint de obtenção de especialidades de médico por ID.");
            return CustomResponse(await _especialidadeMedicoRepository.ObterPorId(id));
        }

        /// <summary>
        /// Adiciona uma nova especialidade de médico.
        /// </summary>
        /// <param name="especialidadeDTO">Os dados da especialidade de médico a ser adicionada.</param>
        /// <returns>A especialidade de médico adicionada ou um erro 400 em caso de falha na adição.</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(EspecialidadeMedico))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Adicionar(EspecialidadeMedicoDTO especialidadeDTO)
        {
            _logger.LogInformation("Endpoint para cadastramento de especialidades do medico.");            
            var validation = await _especialidadeMedicoService.Adicionar(_mapper.Map<EspecialidadeMedico>(especialidadeDTO));
            return validation.IsValid ? Created("", especialidadeDTO) : CustomResponse(validation);
        }

        /// <summary>
        /// Atualiza uma especialidade de médico existente.
        /// </summary>
        /// <param name="especialidadeDTO">Os novos dados da especialidade de médico a ser atualizada.</param>
        /// <returns>Um código 201 em caso de sucesso na atualização ou um erro 400 em caso de falha.</returns>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Atualizar(EspecialidadeMedicoDTO especialidadeDTO)
        {
            _logger.LogInformation("Endpoint para alteração de cadastro da especialidades de médico.");
            return CustomResponse(await _especialidadeMedicoService.Atualizar(_mapper.Map<EspecialidadeMedico>(especialidadeDTO)));
        }

        /// <summary>
        /// Remove uma especialidade de médico por ID.
        /// </summary>
        /// <param name="id">O ID da especialidade de médico a ser removida.</param>
        /// <returns>Um código 201 em caso de sucesso na remoção ou um erro 404 se não encontrada.</returns>
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Remover(Guid id)
        {
            _logger.LogInformation("Endpoint para excluir cadastro de especialidades de médico.");
            return CustomResponse(await _especialidadeMedicoService.Remover(id));
        }
    }
}

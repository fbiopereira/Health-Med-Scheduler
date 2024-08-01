using Health.Core.Controllers;
using HealthMedScheduler.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealthMedScheduler.Api.Controllers
{
    public class AuthController : MainController<AuthController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public AuthController(ILogger<AuthController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Autentica um usuário.
        /// </summary>
        /// <param name="usuarioLogin">Os dados de login do usuário.</param>
        /// <returns>Um token JWT se a autenticação for bem-sucedida ou erros em caso de falha.</returns>

        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(GerarJwtTokenCommand usuarioLogin)
        {
            _logger.LogInformation("Endpoint para login de usuario");

              return CustomResponse(await _mediator.Send(usuarioLogin));
        }
     
    }
}


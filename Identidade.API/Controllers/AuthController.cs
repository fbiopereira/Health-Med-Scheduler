using AloDoutor.Core.Controllers;
using AloDoutor.Core.Identidade;
using Identidade.API.Models;
using Identidade.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identidade.API.Controllers
{
    [Authorize]
    public class AuthController : MainController<AuthController>
    {

        private readonly AuthenticationService _authenticationService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger _logger;

        public AuthController(AuthenticationService authenticationService,
             UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager, ILogger<AuthController> logger) : base(logger)
        {
            _signInManager = signInManager;
            _authenticationService = authenticationService;
            _userManager = userManager;
            _logger = logger;
        }
        
        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="usuarioRegistro">Os dados de registro do usuário.</param>
        /// <returns>Um token JWT se o registro for bem-sucedido ou erros de validação em caso de falha.</returns>
        [ClaimsAuthorize("Administrador", "Cadastrar")]
        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            _logger.LogInformation("Endpoint de registro de usuario");
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

            if (result.Succeeded)
            {
                //Verificar se o usuário cadastrado é administrador
                if (usuarioRegistro.isAdmin)
                {
                    var claim = new Claim("Administrador", "Cadastrar");

                    await _userManager.AddClaimAsync(user, claim);
                }
                return CustomResponse(await _authenticationService.GerarJwt(usuarioRegistro.Email));
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.Description);
            }

            return CustomResponse();
        }

        /// <summary>
        /// Autentica um usuário.
        /// </summary>
        /// <param name="usuarioLogin">Os dados de login do usuário.</param>
        /// <returns>Um token JWT se a autenticação for bem-sucedida ou erros em caso de falha.</returns>
        
        [AllowAnonymous]
        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            _logger.LogInformation("Endpoint para login de usuario");
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha,
                false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authenticationService.GerarJwt(usuarioLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou Senha incorretos");
            return CustomResponse();
        }
    }
}


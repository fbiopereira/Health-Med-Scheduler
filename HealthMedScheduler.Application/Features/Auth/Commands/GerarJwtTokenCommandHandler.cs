using FluentValidation;
using Health.Core.Identidade;
using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Application.Features.Agendamentos.Commands.AdicionarAgendamento;
using HealthMedScheduler.Application.ViewModel.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthMedScheduler.Application.Features.Auth.Commands
{
    public class GerarJwtTokenCommandHandler : IRequestHandler<GerarJwtTokenCommand, UsuarioRespostaLoginViewModel>
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSettings _appSettings;
        private readonly UserManager<IdentityUser> _userManager;

        public GerarJwtTokenCommandHandler(SignInManager<IdentityUser> signInManager, IOptions<AppSettings> appSettings, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }

        public async Task<UsuarioRespostaLoginViewModel> Handle(GerarJwtTokenCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new GerarJwtTokenCommandValidation();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Agendamento inválido", validationResult);


            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha,
               false, true);

            if (result.Succeeded)
            {
                return await GerarJwt(request.Email);
            }

            if (result.IsLockedOut)
            {
                throw new BadRequestException("Usuário temporariamente bloqueado por tentativas inválidas", validationResult);                
            }

            throw new BadRequestException("Usuário ou Senha incorretos", validationResult);
        }

        private async Task<UsuarioRespostaLoginViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodedToken, user, claims);
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
                ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            return identityClaims;
        }

        private UsuarioRespostaLoginViewModel ObterRespostaToken(string encodedToken, IdentityUser user,
          IEnumerable<Claim> claims)
        {
            return new UsuarioRespostaLoginViewModel
            {
                AccessToken = encodedToken,
                //RefreshToken = refreshToken.Token,
                ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
                UsuarioToken = new UsuarioTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}

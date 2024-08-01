using HealthMedScheduler.Application.ViewModel.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMedScheduler.Application.Features.Auth.Commands
{
    public class GerarJwtTokenCommand : IRequest<UsuarioRespostaLoginViewModel>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}

using AloDoutor.Core.Comunication;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace AloDoutor.Core.Controllers
{
    [ApiController]
    public abstract class MainController<T> : Controller
    {
        private readonly ILogger<T> _logger;

        protected MainController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected ICollection<string> Erros = new List<string>();
        protected void LogErro(string mensagem)
        {
            _logger.LogError(mensagem);
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                _logger.LogInformation("Ação concluida com sucesso!");
                return Ok(result);
            }

            LogErro("Erro ao tentar concluir a ação: operação inválida.");
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult resposta)
        {
            ResponsePossuiErros(resposta);

            return CustomResponse();
        }

        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta == null || !resposta.Errors.Mensagens.Any()) return false;

            foreach (var mensagem in resposta.Errors.Mensagens)
            {
                AdicionarErroProcessamento(mensagem);
            }

            return true;
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
            _logger.LogError($"Erro de processamento: {erro}");
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}

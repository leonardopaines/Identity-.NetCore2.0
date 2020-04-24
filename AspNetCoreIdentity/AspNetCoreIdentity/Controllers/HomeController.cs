using AspNetCoreIdentity.Extensions;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KissLog;
using System.Net;
using System;

namespace AspNetCoreIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger logger) => this._logger = logger;

        public IActionResult Index()
        {
            _logger.Info("Hello world from AspNetCore!");
            return View();
        }

        public IActionResult Privacy()
        {

            _logger.Error(new Exception("ERROR ex"));
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClaimExcluir()
        {
            return View("Privacy");
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretClaimGravar()
        {
            return View("Privacy");
        }

        [ClaimsAuthorize("Produtos", "Lers")]
        public IActionResult SecretClaimCustom()
        {
            return View("Privacy");
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel
            {
                ErroCode = id
            };

            switch (id)
            {
                case (int)HttpStatusCode.InternalServerError:
                    modelErro.Titulo = "Ocorreu um erro!";
                    modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                    break;
                case (int)HttpStatusCode.NotFound:
                    modelErro.Titulo = "Ops! Página não encontrada.";
                    modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte.";
                    break;
                case (int)HttpStatusCode.Forbidden:
                    modelErro.Titulo = "Acesso Negado";
                    modelErro.Mensagem = "Você não tem permissão para acessar esse recurso.";
                    break;
                default:
                    return StatusCode(500);
            }

            return View("Error", modelErro);
        }
    }
}

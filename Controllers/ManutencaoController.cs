using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Services;
using WebApplication3.ViewModels.Manutencao;

namespace WebApplication3.Controllers
{
    [Route("api/manutencoes")]
    [ApiController]
    public class ManutencaoController : ControllerBase
    {
        private readonly ManutencaoService _manutencaoService;

        public ManutencaoController(ManutencaoService manutencaoService)
        {
            _manutencaoService = manutencaoService;
        }

        [HttpGet]
        public IActionResult ListarManutencoes()
        {
            return Ok(_manutencaoService.ListarManutencoes());
        }

        [HttpPost]
        public IActionResult AdicionarManutencao(AdicionarManutencaoViewModel dados)
        {
            _manutencaoService.AdicionarManutencao(dados);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarManutencao(int id, AtualizarManutencaoViewModel dados)
        {
            if (id != dados.Id)
                return BadRequest("O id informado na URL é diferente do id informado no corpo da requisição.");

            _manutencaoService.AtualizarManutencao(dados);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirManutencao(int id)
        {
            _manutencaoService.ExcluirManutencao(id);
            return NoContent();
        }
    }
}

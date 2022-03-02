using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Services;
using WebApplication3.ViewModels.Cancelamento;
using WebApplication3.ViewModels.Voo;

namespace WebApplication3.Controllers
{
    [Route("api/voos")]
    [ApiController]
    public class VooController : ControllerBase
    {
        private readonly VooService _vooService;

        public VooController(VooService vooService)
        {
            _vooService = vooService;
        }

        [HttpGet]
        public IActionResult ListarVoos(string? origem, string? destino, DateTime? partida, DateTime? chegada)
        {
            return Ok(_vooService.ListarVoos(origem, destino, partida, chegada));
        }

        [HttpGet("{id}")]
        public IActionResult ListarVooPeloId(int id)
        {
            var voo = _vooService.ListarVooPeloId(id);

            if (voo != null)
                return Ok(voo);
            
            return NotFound();
        }

        [HttpPost]
        public IActionResult AdicionarVoo(AdicionarVooViewModel dados)
        {
            var voo = _vooService.AdicionarVoo(dados);
            return CreatedAtAction(nameof(ListarVooPeloId), new { voo!.Id }, voo);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarVoo(int id, AtualizarVooViewModel dados)
        {
            if (id != dados.Id)
                return BadRequest("O id informado na URL é diferente do id informado no corpo da requisição.");

            var voo = _vooService.ListarVooPeloId(id);

            if (voo != null)
            {
                return Ok(_vooService.AtualizarVoo(dados));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirVoo(int id)
        {
            var voo = _vooService.ListarVooPeloId(id);

            if (voo != null)
            {
                _vooService.ExcluirVoo(id);
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost("cancelar")]
        public IActionResult CancelarVoo(CancelarVooViewModel dados)
        {
            var voo = _vooService.ListarVooPeloId(dados.VooId);

            if (voo != null)
            {
                _vooService.CancelarVoo(dados);
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet("{id}/ficha")]
        public IActionResult ImprimirFichaDoVoo(int id)
        {
            var voo = _vooService.ListarVooPeloId(id);

            if (voo != null)
            {
                var conteudo = _vooService.ImprimirFichaDoVoo(id);
                
                if(conteudo?.Length > 0)
                    return File(conteudo, "application/pdf");

                return NoContent();
            }

            return NotFound();
        }
    }
}

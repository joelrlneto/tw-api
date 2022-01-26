using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Services;
using WebApplication3.ViewModels.Aeronave;

namespace WebApplication3.Controllers
{
    [Route("api/aeronaves")]
    [ApiController]
    public class AeronaveController : ControllerBase
    {
        private readonly AeronaveService _aeronaveService;

        public AeronaveController(AeronaveService aeronaveService)
        {
            _aeronaveService = aeronaveService;
        }

        [HttpGet]
        public IActionResult ListarAeronaves()
        {
            return Ok(_aeronaveService.ListarAeronaves());
        }
        
        [HttpGet("{id}")]
        public IActionResult ListarAeronavePeloId(int id)
        {
            var aeronave = _aeronaveService.ListarAeronavePeloId(id);

            if (aeronave != null)
                return Ok(aeronave);
            return NotFound();
        }

        [HttpPost]
        public IActionResult AdicionarAeronave(AdicionarAeronaveViewModel dados)
        {
            var aeronave = _aeronaveService.AdicionarAeronave(dados);
            return CreatedAtAction(nameof(ListarAeronavePeloId), new { Id = aeronave.Id }, aeronave);
        }
        
        [HttpPut("{id}")]
        public IActionResult AtualizarAeronave(int id, AtualizarAeronaveViewModel dados)
        {
            var aeronave = _aeronaveService.AtualizarAeronave(dados);
            return Ok(aeronave);
        }
        
        [HttpDelete("{id}")]
        public IActionResult ExcluirAeronave(int id)
        {
            _aeronaveService.ExcluirAeronave(id);
            return NoContent();
        }
    }
}

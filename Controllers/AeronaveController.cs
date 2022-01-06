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
            _aeronaveService.AdicionarAeronave(dados);
            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult AtualizarAeronave(int id, AtualizarAeronaveViewModel dados)
        {
            _aeronaveService.AtualizarAeronave(dados);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult ExcluirAeronave(int id)
        {
            _aeronaveService.ExcluirAeronave(id);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Services;
using WebApplication3.ViewModels.Piloto;

namespace WebApplication3.Controllers
{
    [Route("api/pilotos")]
    [ApiController]
    public class PilotoController : ControllerBase
    {
        private readonly PilotoService _pilotoService;

        public PilotoController(PilotoService pilotoService)
        {
            _pilotoService = pilotoService;
        }

        [HttpGet]
        public IActionResult ListarPilotos()
        {
            return Ok(_pilotoService.ListarPilotos());
        }

        [HttpGet("{id}")]
        public IActionResult ListarPilotoPeloId(int id)
        {
            var piloto = _pilotoService.ListarPilotoPeloId(id);

            if (piloto != null)
                return Ok(piloto);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult AdicionarPiloto(AdicionarPilotoViewModel dados)
        {
            _pilotoService.AdicionarPiloto(dados);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarPiloto(int id, AtualizarPilotoViewModel dados)
        {
            _pilotoService.AtualizarPiloto(dados);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirPiloto(int id)
        {
            _pilotoService.ExcluirPiloto(id);
            return NoContent();
        }
    }
}

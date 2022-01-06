﻿using Microsoft.AspNetCore.Http;
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
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult AdicionarVoo(AdicionarVooViewModel dados)
        {
            _vooService.AdicionarVoo(dados);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarVoo(int id, AtualizarVooViewModel dados)
        {
            _vooService.AtualizarVoo(dados);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirVoo(int id)
        {
            _vooService.ExcluirVoo(id);
            return NoContent();
        }

        [HttpPost("cancelar")]
        public IActionResult CancelarVoo(CancelarVooViewModel dados)
        {
            _vooService.CancelarVoo(dados);
            return Ok();
        }

        [HttpGet("{id}/ficha")]
        public IActionResult ImprimirFichaDoVoo(int id)
        {
            var conteudo = _vooService.ImprimirFichaDoVoo(id);
            return File(conteudo, "application/pdf");
        }
    }
}

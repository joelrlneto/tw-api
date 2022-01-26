using DinkToPdf;
using DinkToPdf.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApplication3.Contexts;
using WebApplication3.Entities;
using WebApplication3.Validators.Cancelamento;
using WebApplication3.Validators.Voo;
using WebApplication3.ViewModels.Aeronave;
using WebApplication3.ViewModels.Cancelamento;
using WebApplication3.ViewModels.Piloto;
using WebApplication3.ViewModels.Voo;

namespace WebApplication3.Services
{
    public class VooService
    {
        private readonly CiaAereaContext _context;
        private readonly AdicionarVooValidator _adicionarVooValidator;
        private readonly AtualizarVooValidator _atualizarVooValidator;
        private readonly ExcluirVooValidator _excluirVooValidator;
        private readonly CancelarVooValidator _cancelarVooValidator;
        private readonly IConverter _converter;

        public VooService(CiaAereaContext context, AdicionarVooValidator adicionarVooValidator, AtualizarVooValidator atualizarVooValidator, ExcluirVooValidator excluirVooValidator, CancelarVooValidator cancelarVooValidator, IConverter converter)
        {
            _context = context;
            _adicionarVooValidator = adicionarVooValidator;
            _atualizarVooValidator = atualizarVooValidator;
            _excluirVooValidator = excluirVooValidator;
            _cancelarVooValidator = cancelarVooValidator;
            _converter = converter;
        }

        public DetalhesVooViewModel? AdicionarVoo(AdicionarVooViewModel dados)
        {
            _adicionarVooValidator.ValidateAndThrow(dados);

            var voo = new Voo
            {
                Origem = dados.Origem,
                Destino = dados.Destino,
                DataHoraPartida = dados.DataHoraPartida,
                DataHoraChegada = dados.DataHoraChegada,
                AeronaveId = dados.AeronaveId,
                PilotoId = dados.PilotoId
            };

            _context.Add(voo);
            _context.SaveChanges();

            return ListarVooPeloId(voo.Id);
        }

        public DetalhesVooViewModel? AtualizarVoo(AtualizarVooViewModel dados)
        {
            _atualizarVooValidator.ValidateAndThrow(dados);

            var voo = _context.Voos.Find(dados.Id);

            if (voo != null)
            {
                voo.Origem = dados.Origem;
                voo.Destino = dados.Destino;
                voo.DataHoraPartida = dados.DataHoraPartida;
                voo.DataHoraChegada = dados.DataHoraChegada;
                voo.AeronaveId = dados.AeronaveId;
                voo.PilotoId = dados.PilotoId;

                _context.Update(voo);
                _context.SaveChanges();

                return ListarVooPeloId(voo.Id);
            }
            else
                return null;
        }

        public void ExcluirVoo(int id)
        {
            _excluirVooValidator.ValidateAndThrow(id);

            var voo = _context.Voos.Find(id);

            if (voo != null)
            {
                _context.Remove(voo);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ListarVooViewModel> ListarVoos(string? origem, string? destino, DateTime? partida, DateTime? chegada)
        {
            var filtroOrigem = (Voo voo) => string.IsNullOrWhiteSpace(origem) ? true : voo.Origem == origem;
            var filtroDestino = (Voo voo) => string.IsNullOrWhiteSpace(destino) ? true : voo.Destino == destino;
            var filtroPartida = (Voo voo) => !partida.HasValue ? true : voo.DataHoraPartida >= partida;
            var filtroChegada = (Voo voo) => !chegada.HasValue ? true : voo.DataHoraChegada <= chegada;

            return _context.Voos.Where(filtroOrigem)
                                .Where(filtroDestino)
                                .Where(filtroPartida)
                                .Where(filtroChegada)
                                .Select(v => new ListarVooViewModel
                                {
                                    Id = v.Id,
                                    Origem = v.Origem,
                                    Destino = v.Destino,
                                    DataHoraPartida = v.DataHoraPartida, 
                                    DataHoraChegada = v.DataHoraChegada
                                });
        }

        public DetalhesVooViewModel? ListarVooPeloId(int id)
        {
            var voo = _context.Voos.Where(v => v.Id == id)
                                   .Include(v => v.Aeronave)
                                   .Include(v => v.Piloto)
                                   .Include(v => v.Cancelamento)
                                   .FirstOrDefault();

            if (voo != null)
            {
                return new DetalhesVooViewModel
                {
                    Id = voo.Id,
                    Origem = voo.Origem,
                    Destino = voo.Destino,
                    DataHoraPartida = voo.DataHoraPartida,
                    DataHoraChegada = voo.DataHoraChegada,
                    Aeronave = new DetalhesAeronaveViewModel
                    {
                        Id = voo.Aeronave!.Id,
                        Codigo = voo.Aeronave.Codigo,
                        Fabricante = voo.Aeronave.Fabricante,
                        Modelo = voo.Aeronave.Modelo
                    },
                    Piloto = new DetalhesPilotoViewModel
                    {
                        Id = voo.Piloto!.Id,
                        Matricula = voo.Piloto.Matricula,
                        Nome = voo.Piloto.Nome
                    },
                    Cancelamento = voo.Cancelamento != null ? new DetalhesCancelamentoViewModel
                    {
                        Id = voo.Cancelamento.Id,
                        DataHoraNotificacao = voo.Cancelamento.DataHoraNotificacao,
                        Motivo = voo.Cancelamento.Motivo
                    } : null
                };
            }
            else
                return null;
        }

        public void CancelarVoo(CancelarVooViewModel dados)
        {
            _cancelarVooValidator.ValidateAndThrow(dados);

            var cancelamento = new Cancelamento
            {
                VooId = dados.VooId,
                Motivo = dados.Motivo,
                DataHoraNotificacao = dados.DataHoraNotificacao
            };

            _context.Add(cancelamento);
            _context.SaveChanges();
        }

        public byte[]? ImprimirFichaDoVoo(int id)
        {
            var voo = _context.Voos.Where(v => v.Id == id)
                                   .Include(v => v.Aeronave)
                                   .Include(v => v.Piloto)
                                   .Include(v => v.Cancelamento)
                                   .FirstOrDefault();

            if(voo != null)
            {
                var builder = new StringBuilder();
                builder.Append($"<h1 style='text-align: center'>Ficha do Voo { voo.Id.ToString().PadLeft(10, '0') }</h1>");
                builder.Append($"<hr>");
                builder.Append($"<p><b>ORIGEM:</b> { voo.Origem } (saída em { voo.DataHoraPartida.ToString("dd/MM/yyyy")} às { voo.DataHoraPartida.ToString("hh:mm")})</p>");
                builder.Append($"<p><b>DESTINO:</b> { voo.Destino} (chegada em { voo.DataHoraChegada.ToString("dd/MM/yyyy")} às { voo.DataHoraChegada.ToString("hh:mm")})</p>");
                builder.Append($"<hr>");
                builder.Append($"<p><b>AERONAVE:</b> { voo.Aeronave!.Codigo } ({ voo.Aeronave.Fabricante } { voo.Aeronave.Modelo })</p>");
                builder.Append($"<hr>");
                builder.Append($"<p><b>PILOTO:</b> { voo.Piloto!.Nome } ({ voo.Piloto.Matricula})</p>");
                builder.Append($"<hr>");
                if (voo.Cancelamento != null)
                {
                    builder.Append($"<p style='color: red'><b>VOO CANCELADO:</b> { voo.Cancelamento.Motivo }</p>");
                }
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4
                    },
                    Objects = {
                        new ObjectSettings() {
                            PagesCount = true,
                            HtmlContent = builder.ToString(),
                            WebSettings = { DefaultEncoding = "utf-8" }
                        }
                    }
                };
                return _converter.Convert(doc);
            }
            else
                return null;
        }
    }
}

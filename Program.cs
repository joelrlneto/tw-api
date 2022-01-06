using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Contexts;
using WebApplication3.Services;
using WebApplication3.Validators.Aeronave;
using WebApplication3.Validators.Cancelamento;
using WebApplication3.Validators.Manutencao;
using WebApplication3.Validators.Piloto;
using WebApplication3.Validators.Voo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CiaAereaContext>(options => options.UseSqlServer(@"Server=Localhost\SQLExpress;Database=DbCiaAerea;Integrated Security=True;"));
builder.Services.AddTransient<AeronaveService>();
builder.Services.AddTransient<PilotoService>();
builder.Services.AddTransient<VooService>();
builder.Services.AddTransient<ManutencaoService>();
builder.Services.AddTransient<AdicionarAeronaveValidator>();
builder.Services.AddTransient<AtualizarAeronaveValidator>();
builder.Services.AddTransient<ExcluirAeronaveValidator>();
builder.Services.AddTransient<AdicionarPilotoValidator>();
builder.Services.AddTransient<AtualizarPilotoValidator>();
builder.Services.AddTransient<ExcluirPilotoValidator>();
builder.Services.AddTransient<AdicionarVooValidator>();
builder.Services.AddTransient<AtualizarVooValidator>();
builder.Services.AddTransient<ExcluirVooValidator>();
builder.Services.AddTransient<CancelarVooValidator>();
builder.Services.AddTransient<AdicionarManutencaoValidator>();
builder.Services.AddTransient<AtualizarManutencaoValidator>();
builder.Services.AddTransient<ExcluirManutencaoValidator>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
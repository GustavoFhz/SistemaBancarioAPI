using Microsoft.EntityFrameworkCore;
using SistemaBancario.Data;
using SistemaBancario.Services;
using SistemaBancario.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IClienteIterface, ClienteService>();
builder.Services.AddScoped<IContaInterface, ContaService>();
builder.Services.AddScoped<ICartaoInterface, CartaoServices>();
builder.Services.AddScoped<ITransacaoInterface,  TransacaoService>();
builder.Services.AddScoped<IEmprestimoInterface, EmprestimoService>();

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

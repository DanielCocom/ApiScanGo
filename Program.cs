using api_scango.Domain.Entities;
using api_scango.Domain.Services;
using api_scango.Infrastructure.Data;
using api_scango.Infrastructure.Data.Repositories;
using api_scango.Infrastructure.Data.Repositories.administrador;
using api_scango.Services.Fetures.administrador;
using api_scango.Services.Fetures.compra;
using api_scango.Services.Fetures.establecimiento;

// using api_scango.Services.Fetures.establecimiento;
using api_scango.Services.Fetures.producto;
using api_scango.Services.Mappings;
using api_scango.Services.stripe;

// using api_scango.Services.stripe;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Stripe;


var     builder = WebApplication.CreateBuilder(args);
var Configurations = builder.Configuration;

// para hacer peticiones
builder.Services.AddHttpClient(); 
builder.Services.AddScoped<StripeServices>();




builder.Services.AddScoped<ProductoService>();
builder.Services.AddTransient<ProductoRepository>();

builder.Services.AddScoped<CloudinaryServices>();
builder.Services.AddScoped<EmailService>();


// Clientes
builder.Services.AddScoped<ClienteService>();
builder.Services.AddTransient<ClienteRepository>();

builder.Services.AddScoped<CarritoService>();
builder.Services.AddTransient<CarritoRepository>();

builder.Services.AddScoped<EstablecimientoService>();
builder.Services.AddTransient<EstablecimientoRepository>();

builder.Services.AddScoped<AdministradorService>();
builder.Services.AddTransient<AdministradorRepository>();

builder.Services.AddScoped<VentaServices>();
builder.Services.AddTransient<VentaRepository>();

builder.Services.AddScoped<CompraService>();
builder.Services.AddTransient<CompraRepository>();


builder.Services.AddScoped<ReporteServices>();
builder.Services.AddTransient<ReporteRepository>();
// 
// 

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

builder.Services.AddControllers();
builder.Services.AddDbContext<ScanGoDbContext>(
    options => {
        options.UseSqlServer(Configurations.GetConnectionString("gemDevelopment"));
    }
);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});

//servicio unico para cloudonari, una instancia
builder.Services.AddSingleton(cloudinary => new Cloudinary(new CloudinaryDotNet.Account // para que no haya problemas
{
    Cloud = Configurations["Cloudinary:CloudName"],
    ApiKey = Configurations["Cloudinary:ApiKey"],
    ApiSecret = Configurations["Cloudinary:ApiSecret"]

}));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ReponseMappingProfile).Assembly);
builder.Services.AddControllers();
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();


app.Run();

using GestorEventos.Servicios.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IServicioDeServicios, ServicioDeServicios>();
builder.Services.AddScoped<IServicioPersonas, ServicioPersonas>();
builder.Services.AddScoped<IServicioEventos, ServicioEventos>();
builder.Services.AddScoped<IServicioProvincia, ServicioProvincia>();
builder.Services.AddScoped<IServicioTiposDeEventos, ServicioTiposDeEventos>();
builder.Services.AddScoped<IServicioLocalidad, ServicioLocalidad>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAnyOrigin");


app.MapControllers();

app.Run();

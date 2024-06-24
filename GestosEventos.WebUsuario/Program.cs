using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IServicioEventos, ServicioEventos>();
builder.Services.AddScoped<IServicioPersonas, ServicioPersonas>();
builder.Services.AddScoped<IServicioDeServicios, ServicioDeServicios>();
builder.Services.AddScoped<IServicioUsuarios, ServicioUsuarios>();
builder.Services.AddScoped<IEventosServiciosService, EventosServiciosService>();

//-- Autenticacion con google---//

builder.Services.AddAuthentication(opciones =>
{
    opciones.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opciones.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opciones.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, opciones =>
{
    opciones.ClientId = builder.Configuration.GetSection("GooglaKeys:ClientId").Value; // + ".apps.googleusercontent.com";
    opciones.ClientSecret = builder.Configuration.GetSection("GooglaKeys:ClientPriv").Value;

    opciones.Events.OnCreatingTicket = ctx =>
       {
           var usuarioServicio = ctx.HttpContext.RequestServices.GetRequiredService<IServicioUsuarios>();

           string googleNameIdentifier = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString();

           var usuario = usuarioServicio.GetUsuarioPorGoogleSubject(googleNameIdentifier);
           int idUsuario = 0;
           if (usuario == null)
           {
               Usuario usuarioNuevo = new Usuario();
               usuarioNuevo.Apellido = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value.ToString();
               usuarioNuevo.Nombre = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value.ToString();
               usuarioNuevo.NombreCompleto = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value.ToString();
               usuarioNuevo.GoogleIdentificador = googleNameIdentifier;
               usuarioNuevo.Borrado = false;
               usuarioNuevo.Email = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();

               idUsuario = usuarioServicio.AgregarNuevoUsuario(usuarioNuevo);

           }
           else
           {
               idUsuario = usuario.IdUsuario;
           }
           //ctx.Identity.
           //   usuarioServicio.GetUsuarioPorGoogleSubject(ctx.Identity.Claims)
           // Agregar reclamaciones personalizadas aquí
           ctx.Identity.AddClaim(new System.Security.Claims.Claim("usuarioSolterout", idUsuario.ToString()));

           var accessToken = ctx.AccessToken;
           ctx.Identity.AddClaim(new System.Security.Claims.Claim("accessToken", accessToken));

           return Task.CompletedTask;
       };
});

///----Fin autenticacion---//////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

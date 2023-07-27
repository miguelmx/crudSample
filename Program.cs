using crudSample;
using crudSample.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
("BasicAuthentication", null);
builder.Services.AddAuthorization();


builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionString")));



//builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { 
    Title = "crudSample", 
    Version = "v1"
    Description = "Una pequeña prueba de concepto utilizando las clases Autor y Libro para desmostrar la creacion de una api rest con algunos endpoints que incluya autenticacion basica utilizando Microsoft.AspNetCore.Authentication con un usuario estatico utilizando el decordor [Authorize] en los endpoints que lo requieran e informacion para generar documentacion para Swagger (incluido en el proyecto) para pruebas de los endpoints.",
    Contact = new OpenApiContact
    {
        Name = "Miguel Angel Heredia (desarrollador)",
        Url = new Uri("https://example.com/contact")
    },
    License = new OpenApiLicense
    {
        Name = "Example License",
        Url = new Uri("https://example.com/license")
    }
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

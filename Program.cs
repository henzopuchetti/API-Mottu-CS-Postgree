using Microsoft.EntityFrameworkCore;
using MottuApi.Data;

var builder = WebApplication.CreateBuilder(args);
var MyAllowAnyOriginPolicy = "_myAllowAnyOriginPolicy";

// Força a aplicação a rodar na porta 8080 (não privilegiada, funciona no ACI sem root)
builder.WebHost.UseUrls("http://+:8080");

// Configura o DbContext com a string de conexão vinda do ambiente
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DB_CONNECTION") 
        ?? Environment.GetEnvironmentVariable("DB_CONNECTION")));

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Configura Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
});

// Libera CORS para qualquer origem
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAnyOriginPolicy,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Ativa Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Aplica política de CORS
app.UseCors(MyAllowAnyOriginPolicy);

// Mapeia os endpoints dos controllers
app.MapControllers();

// Inicia a aplicação
app.Run();

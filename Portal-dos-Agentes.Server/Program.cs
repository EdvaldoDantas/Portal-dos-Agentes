using Portal_dos_Agentes.Server;
using Portal_dos_Agentes.Server.Context;
using Portal_dos_Agentes.Server.Repositories.Interfaces;
using Portal_dos_Agentes.Server.Repositories.Repositories;
using Portal_dos_Agentes.Server.Services.Interfaces;
using Portal_dos_Agentes.Server.Services.Services;
using Portal_dos_Agentes.Server.Services.Utilities;
using AuthLibrary.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Buscando as vari�veis de ambiente
var CONNECTIONSTRING = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS") ?? throw new Exception("N�o h� uma conex�o definida");
// var connectionString = "Host=dpg-d1cjbiemcj7s739tr150-a.oregon-postgres.render.com;Port=5432;Database=sgadb_rj2v;Username=sgadb_rj2v_user;Password=aBu5dv49xxY5skMbIqybUSI63ZJ53thE;SSL Mode=Require;Trust Server Certificate=true";
var key = Environment.GetEnvironmentVariable("JWTKey") ?? "EMLEKRMFwefj3nWEDKM3452KELkmkwmk3MLWKEMKWMkm566343kmmd";
var isSwaggerProductionOpen = Environment.GetEnvironmentVariable("SwaggerProductionOpen") == "true";


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerAuth(title: "sga", version: "v1", desc: "Api para Gerenciamente de Agentes do cota s�lva");

//Adicionando o DbContext
//string connectionString = "Host=dpg-d31k6ijipnbc73cdf3a0-a.oregon-postgres.render.com;Port=5432;Database=sgadb_yf9i;Username=agentes;Password=Imh2pCPobrbykLLA7tfmvZJ8wNVt7PHT;SSL Mode=Require;Trust Server Certificate=true;"; 
string connectionString = "Server=tcp:ekmtechserver.database.windows.net,1433;Initial Catalog=sgadb;Persist Security Info=False;User ID=ekmtech;Password=@edvaldo123@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, npgsqlOptions => npgsqlOptions.CommandTimeout(60))
        .EnableSensitiveDataLogging(false));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


//Adicionando suporte a autentica��o
builder.Services.AddAuthSuporte(key, "Silva", "Agentes");
builder.Services.AddTokenService(key);

//Adicionar os reposit�rios e Servi�os
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IAuthManager, AuthManager>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserLoginsRepository, UserLoginsRepository>();
builder.Services.AddScoped<IMesRepository, MesRepository>();
builder.Services.AddScoped<IMesService, MesService>();
builder.Services.AddScoped<IUserLoginsService, UserLoginsService>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IDashBoardRepository, DashBoardRepository>();
builder.Services.AddScoped<IDashBoardAgenteRepository, DashBoardAgenteRepository>();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || isSwaggerProductionOpen)
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGlobalErrorHandling();

app.UseCors("AllowAllOrigins");

app.MapPost("createAdm",  (ApplicationDbContext context) =>
{
    context.Users.Add(new Portal_dos_Agentes.Server.Models.User
    {
        Email = "adm@gmail.com",
        CreatedAt = DateTime.UtcNow,
        Endereco = "Luanda/Cacuaco",
        Id = 1000,
        Role = "adm",
        Telefone = "936008338",
        Senha = HashHelper.Hash("adm123"),
        Nome = "adm",
        IsEmailConfirmed = true
    });
    context.SaveChanges();

    return Results.Ok(context.Users.FirstOrDefault(c => c.Role == "adm"));
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

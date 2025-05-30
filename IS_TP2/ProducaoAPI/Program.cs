using Microsoft.AspNetCore.HttpsPolicy;

var builder = WebApplication.CreateBuilder(args);

// Desativa quase todos os logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole(options => {
    options.LogToStandardErrorThreshold = LogLevel.Error; // Só mostra erros graves
});

// Desativa completamente o redirecionamento HTTPS em desenvolvimento
if (builder.Environment.IsDevelopment())
{
    builder.Services.Configure<HttpsRedirectionOptions>(options => {
        options.HttpsPort = null;
    });
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

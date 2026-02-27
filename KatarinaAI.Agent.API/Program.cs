using Google.GenAI;
using KatarinaAI.Agent.Application.Interfaces;
using KatarinaAI.Agent.Application.UserCases;
using KatarinaAI.Agent.Infrastructure.Configuration;
using KatarinaAI.Agent.Infrastructure.Embeddings;
using KatarinaAI.Agent.Infrastructure.LLMProviders;
using KatarinaAI.Agent.Infrastructure.VectorStore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<GeminiOptions>(
    builder.Configuration.GetSection("Gemini"));

builder.Services.AddSingleton<Client>(sp =>
{
    var options = sp.GetRequiredService<IOptions<GeminiOptions>>().Value;

    Environment.SetEnvironmentVariable("GOOGLE_API_KEY", options.ApiKey);

    return new Client();
});

builder.Services.AddScoped<ILLMProvider, GeminiProvider>();

builder.Services.AddScoped<GenerateTicketUseCase>();

builder.Services.AddSingleton<IVectorStore, InMemoryVectorStore>();
builder.Services.AddScoped<IEmbeddingProvider, GeminiEmbeddingProvider>();

builder.Services.Configure<GitHubSettings>(
    builder.Configuration.GetSection("GitHub"));

builder.Services.Configure<ProjectSettings>(
    builder.Configuration.GetSection("Project"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

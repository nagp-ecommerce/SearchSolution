using Microsoft.EntityFrameworkCore;
using OpenSearch.Client;
using SearchApiService.Interfaces;
using SearchApiService.Services;
using SharedService.Lib.DI;

var builder = WebApplication.CreateBuilder(args);

SharedServicesContainer
    .AddSharedServices<DbContext>(builder.Services, builder.Configuration);

builder.Services.AddSingleton<IOpenSearchClient>(s =>
    OpenSearchClientFactory.openSearchClient(builder.Configuration)
);

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
SharedServicesContainer.UseSharedPolicies(app);

app.Run();

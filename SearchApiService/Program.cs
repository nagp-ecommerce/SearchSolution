using Amazon.SimpleNotificationService;
using Microsoft.EntityFrameworkCore;
using OpenSearch.Client;
using SearchApiService.Services;
using SharedService.Lib.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IOpenSearchClient>(s => 
    OpenSearchClientFactory.openSearchClient(builder.Configuration)
);

SharedServicesContainer.AddSharedServices<DbContext>(builder.Services, builder.Configuration);

//JwtAuthenticationScheme.AddJwtAuthenticationScheme(builder.Services, builder.Configuration);

//// setting up AWS SNS asynchronous communication
//builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
//builder.Services.AddAWSService<IAmazonSimpleNotificationService>();

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

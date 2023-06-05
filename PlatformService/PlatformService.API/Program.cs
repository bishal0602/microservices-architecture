using PlatformService.API;
using PlatformService.API.Helpers;
using PlatformService.Domain;
using PlatformService.Infrastructure;
using PlatformService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddInfrastructure(builder.Configuration,
                       builder.Environment.IsProduction())
    .AddPresentation();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    InMemDbHelper.PopulateDate(app);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<PlatformService.Infrastructure.SyncDataServices.Grpc.GrpcPlatformService>();
app.MapGet("/protos/platforms.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("../PlatformService.Infrastructure/SyncDataServices/Grpc/Protos/platforms.proto"));
});

app.Run();

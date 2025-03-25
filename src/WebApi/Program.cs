using CleanArchitecture.Application;
using Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;
using WebApi;
using WebApi.Controllers;
using WebApi.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddOpenApi();
builder.Services.AddRateLimiter(x => x.AddFixedWindowLimiter("fixed", cfg =>
{
    cfg.QueueLimit = 100;
    cfg.Window = TimeSpan.FromSeconds(1);
    cfg.PermitLimit = 100;
    cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
}));
builder.Services.AddControllers().AddOData(opt =>

    opt
    .Select()
    .Filter()
    .Count()
    .Expand()
    .OrderBy()
    .SetMaxTop(null)
    .AddRouteComponents("odata", AppODataController.GetEdmModel())
);

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();


app.MapOpenApi();
app.MapScalarApiReference();

app.MapDefaultEndpoints();

app.UseCors(x => x.
AllowAnyHeader().
AllowCredentials().
AllowAnyMethod().
SetIsOriginAllowed(origin => true));

app.MapControllers().RequireRateLimiting("fixed");

app.RegisterRoutes();

app.UseExceptionHandler();

app.Run();

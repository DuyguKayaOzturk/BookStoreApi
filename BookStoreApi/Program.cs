using BookStoreApi.Repository;
using Microsoft.AspNetCore.Mvc;
using BookStoreApi.Endpoints;
using BookStoreApi.Model;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookRepository, BookDbHandler>();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var logger = (ILogger<Program>)app.Services.GetService(typeof(ILogger<Program>))!;


app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Something went wrong - test exception {@Machine} {@TraceId}",
               Environment.MachineName,
               System.Diagnostics.Activity.Current?.Id);

        await Results.Problem(
            title: "Something terrible has happened !!",
            statusCode: StatusCodes.Status500InternalServerError,
            extensions: new Dictionary<string, Object?>
            {
                    { "traceId", System.Diagnostics.Activity.Current?.Id },
            })
            .ExecuteAsync(context);
    }

});

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();


app.MapBookEndpoints();


// https://localhost:7125/filtertest?start=3&stop=10
app.MapGet("/filtertest", ([FromQuery] int? start, [FromQuery] int? stop) =>
{
    return $"hello, start={start}, stop={stop}";
});

// HTTP metode: GET
app.MapGet("/httpheader", (HttpContext context) =>
{
    return $"{context.Request.Host.Value}, \n{context.Request.Headers["User-Agent"]}, " +
    $"\n{context.Request.Headers["Accept"]}";
})
.WithOpenApi()
.WithName("GetHttpHeaderInfo");

app.Run();



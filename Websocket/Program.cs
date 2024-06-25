using Microsoft.AspNetCore.SignalR;
using Websocket;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option =>
{
    option.AddPolicy("app", builder =>
    {
        builder.WithOrigins(["http://localhost:5173", "http://localhost:7071"])
        .AllowAnyHeader()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("notification", async (string message, IHubContext<NotificationHub, INotificationClient> context) =>
{
    await context.Clients.All.ReceiveMessage(message);

    return Results.NoContent();
});

app.MapPost("notificationBody", async (NotificationBody notificationBody, IHubContext<NotificationHub, INotificationClient> context) =>
{
    await context.Clients.All.ReceiveBody(notificationBody);

    return Results.NoContent();
});

app.UseCors("app");

app.UseHttpsRedirection();

app.MapHub<NotificationHub>("notification");

app.Run();

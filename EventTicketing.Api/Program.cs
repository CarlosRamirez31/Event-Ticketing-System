using EventTicketing.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger();

var app = builder.Build();

app.MapSwagger();

app.Run();

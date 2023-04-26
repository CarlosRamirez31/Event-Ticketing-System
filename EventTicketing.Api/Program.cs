using Carter;
using EventTicketing.Api.Extensions;
using EventTicketing.Application;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddCarter();
builder.Services.AddSwagger();
builder.Services.AddPersistence(configuration);

var app = builder.Build();

app.MapSwagger();
app.MapCarter();

app.Run();

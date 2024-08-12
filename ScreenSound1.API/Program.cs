using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.EndPoints;
using ScreenSound.API.NovaPasta2;
using ScreenSound.BD;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>(); //Injeção do Context
builder.Services.AddTransient<DAL<Artista>>();     //Injeção do DAL
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.AddTransient<DAL<Genero>>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.AddEndPointsArtistas();
app.AddEndPointMusicas();
app.AddEndPointsGeneros();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

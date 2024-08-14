using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.BD;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using System.Runtime.CompilerServices;

namespace ScreenSound.API.EndPoints
{
    public static class GenerosExtension
    {
        private static ICollection<GeneroResponse> GeneroToList(IEnumerable<Genero> list) {
            return list.Select(g => EntityToResponse(g)).ToList();
        }

        private static GeneroResponse EntityToResponse(Genero genero) {
            return new GeneroResponse(genero.Id, genero.Nome!, genero.Descricao!);
        }
        public static void AddEndPointsGeneros (this WebApplication app) {

            app.MapGet("/Generos", ([FromServices] DAL<Genero> dal) =>
            {
                var listGeneros = dal.ListWithManyIncludes(q => q.Include(g=>g.Musicas).ThenInclude(g=>g.Artista));
                if (listGeneros != null)
                {
                    var generoToList = GeneroToList(listGeneros);
                    return Results.Ok(generoToList);
                }
                return Results.NotFound();
            });
            app.MapGet("/Generos/{id}", ([FromServices] DAL<Genero> dal, int id) =>
            {
                var generoEncontrado = dal.RecuperarPor(g => g.Id == id);
                if (generoEncontrado != null) {
                    var generoEncontradoToResponse = EntityToResponse(generoEncontrado);
                    return Results.Ok(generoEncontradoToResponse);
                }
                return Results.NotFound();
            });
            app.MapPost("/Generos/Add", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequest generoRequest) => {
                var novoGenero = new Genero()
                {
                    Descricao = generoRequest.Descricao,
                    Nome = generoRequest.Nome
                };
                dal.Add(novoGenero);
                return Results.Ok(novoGenero);
            });
            app.MapPut("/Generos/Edit", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequestEdit generoRequestEdit) =>
            {
                var foundGenero = dal.RecuperarPor(g=>g.Id ==  generoRequestEdit.Id);
                if (foundGenero is not null)
                {
                    var (isValid, errorMessage) = GeneroValidation(generoRequestEdit);
                    if (!isValid)
                    {
                        return Results.BadRequest(errorMessage);
                    }
                    foundGenero.Descricao = generoRequestEdit.Descricao;
                    foundGenero.Nome = generoRequestEdit.Nome; 

                    dal.Update(foundGenero);
                    return Results.Ok();
                }
                return Results.NotFound();
            });
            app.MapDelete("Generos/Delete/{id}", ([FromServices] DAL<Genero> dal, int id) =>
             {
                 var foundGender = dal.RecuperarPor(g => g.Id == id);
                 if (foundGender != null)
                 {
                     dal.Delete(foundGender);
                     return Results.NoContent();
                 }
                 return Results.NotFound();
             });
        }
        static (bool, string) GeneroValidation(GeneroRequestEdit request)
        {
            if (string.IsNullOrEmpty(request.Nome))
            {
                return (false, "O campo 'Nome' não pode ser nulo.");
            }
            return (true, string.Empty);
        }

    }
}

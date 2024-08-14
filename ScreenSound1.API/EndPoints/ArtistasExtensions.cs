using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.BD;
using ScreenSound.Modelos;

namespace ScreenSound.API.NovaPasta2
{
    public static class ArtistasExtensions
    {
        private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> artistaList)
        {
            return artistaList.Select(a => EntityToResponse(a)).ToList();
        }
        private static ArtistaResponse EntityToResponse(Artista artista)
        {
            return new ArtistaResponse(artista.Id, artista.Nome, artista.FotoPerfil, artista.Bio);
        }

        public static void AddEndPointsArtistas(this WebApplication app)
        {
            app.MapGet("/Artista", ([FromServices] DAL<Artista> dal) =>
            {
                var artistList = dal.Listar();
                if (artistList is null) {
                    return Results.NotFound();
                    }
                var artistListReponse = EntityListToResponseList(artistList);
                return Results.Ok(artistListReponse);
            });

            app.MapGet("/Artista/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
            {
                var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (artista != null)
                {
                    return Results.Ok(EntityToResponse(artista));
                }
                return Results.NotFound();
            });
            app.MapPost("/Artista/Add/", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
            {
                var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio);

                dal.Add(artista);
                return Results.Ok();
            });
            app.MapDelete("/Artista/Delete/{id}", ([FromServices] DAL<Artista> dal, int id) =>
            {
                var foundArtist = dal.RecuperarPor(a => a.Id == id);
                if (foundArtist is not null)
                {
                    dal.Delete(foundArtist);
                    return Results.NoContent();
                }
                return Results.NotFound();
            });
            app.MapPut("/Artista/Edit", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequestEdit) =>
            {
                var artistToUpdate = dal.RecuperarPor(a => a.Id == artistaRequestEdit.Id);
                if (artistToUpdate is not null)
                {
                    var (isValid, errorMessage) = ArtistValidation(artistaRequestEdit);
                    if (!isValid)
                    {
                        return Results.BadRequest(errorMessage);
                    }

                    artistToUpdate.Nome = artistaRequestEdit.Nome;
                    artistToUpdate.Bio = artistaRequestEdit.Bio;
                    dal.Update(artistToUpdate);
                    return Results.Ok();
                    
                }
                else
                {
                    return Results.NotFound();
                }
            });

            static (bool, string) ArtistValidation (ArtistaRequestEdit request)
            {
                if (string.IsNullOrEmpty(request.Nome))
                {
                    return (false, "O campo 'Nome' não pode ser nulo.");
                }
                if (string.IsNullOrEmpty(request.Bio))
                {
                    return (false, "O campo 'Bio' não pode ser nulo.");
                }
                return (true, string.Empty);
            };
        }
    }
}

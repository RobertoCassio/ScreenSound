using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.BD;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.NovaPasta2
{
    public static class MusicasExtensions
    {
        #region MusicEndpoint
        private static ICollection<MusicaResponse> MusicaToList(IEnumerable<Musica> musicaList)
        {
            return musicaList.Select(m => EntityToReponse(m)).ToList();
        }

        private static MusicaResponse EntityToReponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista!.Nome);
        }

        public static void AddEndPointMusicas(this WebApplication app)
        {
            app.MapGet("/Musica", ([FromServices] DAL<Musica> dal) =>
            {
                var musicaList = dal.ListWithIncludes(m=> m.Artista);
                if (musicaList is null)
                {
                    return Results.NotFound();
                }
                var musicaListResponse = MusicaToList(musicaList);
                return Results.Ok(musicaListResponse);
            });
            app.MapGet("/Musica/{id}", ([FromServices] DAL<Musica> dal, int id) =>
            {
                var foundMusic = dal.RecuperarPor(m => m.Id == id, m=> m.Artista);
                if (foundMusic is not null)
                {
                    return Results.Ok(EntityToReponse(foundMusic));
                }
                return Results.NotFound();
            });
            app.MapPost("/Musica/Add", ([FromServices] DAL<Musica> dal,[FromServices] DAL<Genero> dalGenero, [FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica(musicaRequest.Nome)
                {
                    AnoLancamento = musicaRequest.ReleaseYear,
                    ArtistaId = musicaRequest.ArtistaId,
                    Generos = musicaRequest.Generos is not null ? GeneroRequestConverter(musicaRequest.Generos, dalGenero) : new List<Genero>(),
                };

                dal.Add(musica);
                return Results.Ok();
            });
            app.MapDelete("/Musica/Delete/{id}", ([FromServices] DAL<Musica> dal, int id) =>
            {
                var foundMusic = dal.RecuperarPor(m => m.Id == id);
                if (foundMusic is not null)
                {
                    dal.Delete(foundMusic);
                    return Results.NoContent();
                }
                return Results.NotFound();
            });
            app.MapPut("Musica/Edit", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequestEdit musicaRequestEdit) =>
            {
                var musicToUpdate = dal.RecuperarPor(m => m.Id == musicaRequestEdit.Id);
                if (musicToUpdate is not null)
                {
                    musicToUpdate.Nome = musicaRequestEdit.Nome;
                    musicToUpdate.AnoLancamento = musicaRequestEdit.AnoLancamento;

                    dal.Update(musicToUpdate);

                    return Results.Ok();
                }
                return Results.NotFound();

            });
            #endregion
        }

        private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dalGenero)
        {
            var listaDeGeneros = new List<Genero>();
            foreach (var genero in generos)
            {
                var entity = RequestToEntity(genero);
                var FoundGenero = dalGenero.RecuperarPor(g=> g.Nome.ToUpper().Equals(genero.Nome.ToUpper()));
                if (FoundGenero is not null)
                {
                    listaDeGeneros.Add(FoundGenero);
                }
                else
                {
                    listaDeGeneros.Add(entity);
                }
            }
                return listaDeGeneros;
            //return generos.Select(a => RequestToEntity(a)).ToList();
        }
        private static Genero RequestToEntity(GeneroRequest genero)
        {
            return new Genero() { Descricao = genero.Descricao, Nome = genero.Nome };
        }
    }
}

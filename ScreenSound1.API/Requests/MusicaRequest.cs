using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests
{
    public record MusicaRequest([Required]string Nome, int ReleaseYear, [Required] int ArtistaId, ICollection<GeneroRequest> Generos=null);
}

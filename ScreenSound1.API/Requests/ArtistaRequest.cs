using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests
{
    public record ArtistaRequest([Required] string Nome, string Bio);

}

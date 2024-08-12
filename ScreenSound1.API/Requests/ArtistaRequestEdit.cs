namespace ScreenSound.API.Requests
{
    public record ArtistaRequestEdit(string Nome, string Bio, int Id) : ArtistaRequest(Nome,Bio);
}

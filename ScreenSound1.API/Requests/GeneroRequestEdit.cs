namespace ScreenSound.API.Requests
{
    public record GeneroRequestEdit (int Id, String Nome, String Descricao) : GeneroRequest(Nome, Descricao);

}

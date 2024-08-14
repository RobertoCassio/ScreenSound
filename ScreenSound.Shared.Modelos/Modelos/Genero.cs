using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Shared.Modelos.Modelos
{
    public class Genero
    {
        public int  Id {  get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; } = string.Empty;

        public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>(); //Virtual para permitir que o Entity controle as duas entidades.

        public override string ToString() //Usa esse método aqui caso precise listar
        {
            return $"Nome: {Nome} Descrição: {Descricao}";
        }
    }
}

using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.BD
{
    internal class MusicaDAL:DAL <Musica>
    {

        private readonly ScreenSoundContext context;

        public MusicaDAL(ScreenSoundContext context)
        {
            this.context = context;
        }

        public override IEnumerable<Musica> ListMusic()
        {
            return context.Musicas.ToList();
        }
        public override void Add (Musica musica)
        {
            context.Musicas.Add(musica);
            context.SaveChanges();
        }
        public override void Delete (Musica musica)
        {
            context.Musicas.Remove(musica);
            context.SaveChanges();      
        }
        public override void Update(Musica musica)
        {
            context.Musicas.Update(musica);
            context.SaveChanges();
        }
        public Musica? RecuperarPeloNome(string nome)
        {
            return context.Musicas.FirstOrDefault(artista => artista.Nome.Contains(nome));
        }
    } 
}

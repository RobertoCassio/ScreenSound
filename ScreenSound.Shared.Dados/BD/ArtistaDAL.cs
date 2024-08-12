using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.BD
{
    internal class ArtistaDAL : DAL<Artista> //Aqui o T é substituído por Artista em DAL<Artista>
    {

        public ArtistaDAL(ScreenSoundContext context) : base(context) { } //Esse base(context) é para herdar o context do DAL

    }
}

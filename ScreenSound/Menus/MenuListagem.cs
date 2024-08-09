using ScreenSound.BD;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Menus
{
    internal class MenuListagem : Menu
    {
        public override void Executar(DAL<Artista> artistaDAL) {
            base.Executar(artistaDAL);
            ExibirTituloDaOpcao("Listar músicas por ano");
            Console.WriteLine("Qual o anos das músicas que deseja buscar? ");
            string yearMusic = Console.ReadLine()!;
            var musicDAL = new DAL<Musica>(new ScreenSoundContext());
            var musicFound = musicDAL.ListByYear(m => m.AnoLancamento == Convert.ToInt32(yearMusic));
            if (musicFound.Any())
            {
                foreach (var m in musicFound)
                {
                    Console.WriteLine($"{m.Nome}, Ano: {m.AnoLancamento}");
                }
                Console.WriteLine("Digite uma tecla para voltar ao Menu Inicial");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Nenhuma música encontrada");
                Console.WriteLine("Digite uma tecla para voltar ao Menu Inicial");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}

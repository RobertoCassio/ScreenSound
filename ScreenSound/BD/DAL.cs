using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.BD
{
    internal class DAL<T> where T : class //Classe abstrata pois não cria nenhum objeto / //T : Class quer dizer que o T tem que ser um classe
    {
        protected readonly ScreenSoundContext context;

        public DAL(ScreenSoundContext context)
        {
            this.context = context;
        }
        // As linhas acima fazem basicamente o mesmo que using var context = new ScreenSoundContext(); em cada função
        public IEnumerable<T> Listar()
        {
            return context.Set<T>().ToList();
        }
        public void Add(T objeto)
        {
            context.Set<T>().Add(objeto);
            context.SaveChanges();
        }

        public void Update(T objeto)
        {
            context.Set<T>().Update(objeto);
            context.SaveChanges();
        }

        public void Delete(T objeto)
        {
            context.Set<T>().Remove(objeto);
            context.SaveChanges();
        }
        public T? RecuperarPor(Func<T, Boolean> condition)
        { //? Pois pode ser nulo
            {
                return context.Set<T>().FirstOrDefault(condition);
            }
        }

        public IEnumerable<T> ListByYear(Func<T, Boolean> condition)
        {
            return context.Set<T>().Where(condition);
        }
    }
}

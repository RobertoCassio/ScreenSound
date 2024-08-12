using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.BD
{
    public class DAL<T> where T : class //Classe abstrata pois não cria nenhum objeto / //T : Class quer dizer que o T tem que ser um classe
    {
        private readonly ScreenSoundContext context;

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
        public T? RecuperarPor(Func<T, Boolean> condition, params Expression<Func<T, object>>[] includes)
        { //? Pois pode ser nulo
            {
                IQueryable<T> valueToRecover = context.Set<T>();
                if (includes != null)
                {
                    foreach (var i in includes)
                    {
                        valueToRecover = valueToRecover.Include(i);
                    }
                }
                return valueToRecover.FirstOrDefault(condition);
            }
        }

        public IEnumerable<T> ListByYear(Func<T, Boolean> condition)
        {
            return context.Set<T>().Where(condition);
        }

        public IEnumerable<T> ListWithIncludes(params Expression<Func<T, object>> [] includes) {
            IQueryable<T> list = context.Set<T>();
            
            if (includes != null)
            {
                foreach(var i in includes)
                {
                    list = list.Include(i);
                }
            }
            return list.ToList();
        }
        public IEnumerable<T> ListWithManyIncludes(params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            IQueryable<T> list = context.Set<T>();
            if (includes != null)
            {
                {
                    foreach(var i in includes)
                    {
                        list = i(list);
                    }
                }
            }
            return list.ToList();
        }
    }
}

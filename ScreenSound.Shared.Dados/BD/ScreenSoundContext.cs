using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.BD
{
    public class ScreenSoundContext: DbContext
    {
        public DbSet<Artista> Artistas { get; set; } // Para o Entity identificar a tabela de Artistas
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Genero> Generos {  get; set; } 

        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSoundV0;Integrated" +
            " Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;" +
            "Multi Subnet Failover=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musica>().HasMany(c => c.Generos).WithMany(c => c.Musicas);
            modelBuilder.Entity<Musica>().HasOne(c => c.Artista).WithMany(c => c.Musicas).HasForeignKey(m => m.ArtistaId); //Somente relacionamentos
        }

    }
}

using Microsoft.EntityFrameworkCore;
using MvcWebSchool_Identity.Models;


namespace MvcWebSchool_Identity.Data
{
    public class WebSchoolContext : DbContext
    {
        public WebSchoolContext(DbContextOptions<WebSchoolContext> opts) 
            : base(opts) { }

        public DbSet<Aluno> Alunos { get; set; }

        //Adiciona um novo registro 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aluno>().HasData(
               new Aluno
               {
                   Id = 1,
                   Nome = "José",
                   Email = "junyor75821@gmail.com",
                   Idade = 23,
                   Curso = "Química"
               });
        }

    }
}

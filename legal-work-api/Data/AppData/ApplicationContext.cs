using legal_work_api.Models;
using Microsoft.EntityFrameworkCore;

namespace legal_work_api.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<FuncionarioEntity> Funcionario { get; set; }
        public DbSet<EmpresaEntity> Empresa { get; set; }
        public DbSet<JornadaEntity> Jornada { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // EMPRESA 1 : N FUNCIONÁRIOS
            modelBuilder.Entity<EmpresaEntity>()
                .HasMany(e => e.Funcionarios)
                .WithOne(f => f.Empresa)
                .HasForeignKey(f => f.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);

            // FUNCIONÁRIO 1 : N JORNADAS
            modelBuilder.Entity<FuncionarioEntity>()
                .HasMany(f => f.Jornadas)
                .WithOne(j => j.Funcionario)
                .HasForeignKey(j => j.FuncionarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

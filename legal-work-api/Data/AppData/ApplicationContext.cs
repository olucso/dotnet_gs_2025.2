using Microsoft.EntityFrameworkCore;
using legal_work_api.Models;

namespace legal_work_api.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<EmpresaEntity> Empresa { get; set; }
        public DbSet<FuncionarioEntity> Funcionario { get; set; }
        public DbSet<JornadaEntity> Jornada { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmpresaEntity>().ToTable("lw_empresa");
            modelBuilder.Entity<FuncionarioEntity>().ToTable("lw_funcionario");
            modelBuilder.Entity<JornadaEntity>().ToTable("lw_jornada");
        }
    }
}

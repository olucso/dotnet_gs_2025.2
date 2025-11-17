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
    }
}

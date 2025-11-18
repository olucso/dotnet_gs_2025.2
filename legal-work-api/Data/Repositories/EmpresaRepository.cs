using legal_work_api.Data.AppData;
using legal_work_api.Data.Repositories.Interfaces;
using legal_work_api.Models;
using Microsoft.EntityFrameworkCore;

namespace legal_work_api.Data.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly ApplicationContext _context;

        public EmpresaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<EmpresaEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3)
        {
            var totalRecords = await _context.Empresa.CountAsync();

            var result = await _context
                .Empresa
                .OrderBy(x => x.Id)
                .Skip(Displacement)
                .Take(TotalRecords)
                .ToListAsync();

            return new PageResultModel<IEnumerable<EmpresaEntity>>
            {
                Data = result,
                Deslocamento = Displacement,
                RegistrosRetornados = TotalRecords,
                TotalRegistros = totalRecords
            };
        }

        public async Task<EmpresaEntity?> GetById(int id)
        {
            var result = await _context.Empresa.FindAsync(id);

            return result;
        }

        public Task<EmpresaEntity?> Add(EmpresaEntity entity)
        {
            _context.Empresa.Add(entity);
            _context.SaveChanges();

            return Task.FromResult<EmpresaEntity?>(entity);
        }

        public async Task<EmpresaEntity?> Update(int Id, EmpresaEntity entity)
        {
            var result = await _context.Empresa.FindAsync(Id);

            if (result is not null)
            {
                result.RazaoSocial = entity.RazaoSocial;
                result.Cnpj = entity.Cnpj;
                result.Endereco = entity.Endereco;
                result.Telefone = entity.Telefone;

                _context.Update(result);
                _context.SaveChanges();

                return result;
            }

            return null;
        }

        public async Task<EmpresaEntity?> Delete(int Id)
        {
            var result = await _context.Empresa.FindAsync(Id);

            if (result is not null)
            {
                _context.Remove(result);
                _context.SaveChanges();

                return result;
            }

            return null;
        }
    }
}

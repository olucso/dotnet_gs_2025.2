using legal_work_api.Data.AppData;
using legal_work_api.Data.Repositories.Interfaces;
using legal_work_api.Models;
using Microsoft.EntityFrameworkCore;

namespace legal_work_api.Data.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly ApplicationContext _context;

        public FuncionarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<FuncionarioEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3)
        {
            var totalRecords = await _context.Funcionario.CountAsync();

            var result = await _context
                .Funcionario
                .OrderBy(x => x.Id)
                .Skip(Displacement)
                .Take(TotalRecords)
                .ToListAsync();

            return new PageResultModel<IEnumerable<FuncionarioEntity>>
            {
                Data = result,
                Deslocamento = Displacement,
                RegistrosRetornados = TotalRecords,
                TotalRegistros = totalRecords
            };
        }

        public async Task<FuncionarioEntity?> GetById(int id)
        {
            var result = await _context.Funcionario.FindAsync(id);

            return result;
        }

        public Task<FuncionarioEntity?> Add(FuncionarioEntity entity)
        {
            _context.Funcionario.Add(entity);
            _context.SaveChanges();

            return Task.FromResult<FuncionarioEntity?>(entity);
        }

        public async Task<FuncionarioEntity?> Update(int Id, FuncionarioEntity entity)
        {
            var result = await _context.Funcionario.FindAsync(Id);

            if (result is not null)
            {
                result.Nome = entity.Nome;
                result.Cpf = entity.Cpf;
                result.Endereco = entity.Endereco;
                result.Telefone = entity.Telefone;
                result.Email = entity.Email;

                _context.Update(result);
                _context.SaveChanges();

                return result;
            }

            return null;
        }

        public async Task<FuncionarioEntity?> Delete(int Id)
        {
            var result = await _context.Funcionario.FindAsync(Id);

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

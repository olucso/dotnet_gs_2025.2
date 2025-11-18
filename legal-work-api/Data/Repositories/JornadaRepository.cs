using legal_work_api.Data.AppData;
using legal_work_api.Data.Repositories.Interfaces;
using legal_work_api.Models;
using Microsoft.EntityFrameworkCore;

namespace legal_work_api.Data.Repositories
{
    public class JornadaRepository : IJornadaRepository
    {
        private readonly ApplicationContext _context;

        public JornadaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<JornadaEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3)
        {
            var totalRecords = await _context.Jornada.CountAsync();

            var result = await _context
                .Jornada
                .OrderBy(x => x.Id)
                .Skip(Displacement)
                .Take(TotalRecords)
                .ToListAsync();

            return new PageResultModel<IEnumerable<JornadaEntity>>
            {
                Data = result,
                Deslocamento = Displacement,
                RegistrosRetornados = TotalRecords,
                TotalRegistros = totalRecords
            };
        }

        public async Task<JornadaEntity?> GetById(int id)
        {
            var result = await _context.Jornada.FindAsync(id);

            return result;
        }

        public Task<JornadaEntity?> Add(JornadaEntity entity)
        {
            _context.Jornada.Add(entity);
            _context.SaveChanges();

            return Task.FromResult<JornadaEntity?>(entity);
        }

        public async Task<JornadaEntity?> Update(int Id, JornadaEntity entity)
        {
            var result = await _context.Jornada.FindAsync(Id);

            if (result is not null)
            {
                result.DataInicio = entity.DataInicio;
                result.HorasTrabalhadas = entity.HorasTrabalhadas;

                _context.Update(result);
                _context.SaveChanges();

                return result;
            }

            return null;
        }

        public async Task<JornadaEntity?> Delete(int Id)
        {
            var result = await _context.Jornada.FindAsync(Id);

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

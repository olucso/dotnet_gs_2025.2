using legal_work_api.Models;

namespace legal_work_api.Data.Repositories.Interfaces
{
    public interface IJornadaRepository
    {
        Task<PageResultModel<IEnumerable<JornadaEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3);
        Task<JornadaEntity?> GetById(int id);
        Task<JornadaEntity?> Add(JornadaEntity entity);
        Task<JornadaEntity?> Update(int Id, JornadaEntity entity);
        Task<JornadaEntity?> Delete(int Id);
    }
}

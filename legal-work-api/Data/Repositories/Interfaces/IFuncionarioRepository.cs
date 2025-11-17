using legal_work_api.Models;

namespace legal_work_api.Data.Repositories.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<PageResultModel<IEnumerable<FuncionarioEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3);
        Task<FuncionarioEntity?> GetById(int id);
        Task<FuncionarioEntity?> Add(FuncionarioEntity entity);
        Task<FuncionarioEntity?> Update(int Id, FuncionarioEntity entity);
        Task<FuncionarioEntity?> Delete(int Id);
    }
}

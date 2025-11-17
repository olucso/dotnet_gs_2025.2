using legal_work_api.Models;

namespace legal_work_api.Data.Repositories.Interfaces
{
    public interface IEmpresaEntity
    {
        Task<PageResultModel<IEnumerable<EmpresaEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3);
        Task<EmpresaEntity?> GetById(int id);
        Task<EmpresaEntity?> Add(EmpresaEntity entity);
        Task<EmpresaEntity?> Update(int Id, EmpresaEntity entity);
        Task<EmpresaEntity?> Delete(int Id);
    }
}

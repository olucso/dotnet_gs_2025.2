using legal_work_api.Dtos;
using legal_work_api.Models;

namespace legal_work_api.Mappers
{
    public static class EmpresaMapper
    {
        public static EmpresaEntity ToEmpresaEntity(this EmpresaDto e)
        {
            return new EmpresaEntity()
            {
                RazaoSocial = e.RazaoSocial,
                Cnpj = e.Cnpj,
                Endereco = e.Endereco,
                Telefone = e.Telefone,
            };
        }
    }
}

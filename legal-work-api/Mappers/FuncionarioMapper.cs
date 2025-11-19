using legal_work_api.Dtos;
using legal_work_api.Models;

namespace legal_work_api.Mappers
{
    public static class FuncionarioMapper
    {
        public static FuncionarioEntity ToFuncionarioEntity(this FuncionarioDto f)
        {
            return new FuncionarioEntity()
            {
                Nome = f.Nome,
                Cpf = f.Cpf,
                Endereco = f.Endereco,
                Telefone = f.Telefone,
                Email = f.Email,
            };
        }
    }
}

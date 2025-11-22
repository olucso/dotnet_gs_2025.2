using legal_work_api.Dtos;
using legal_work_api.Models;

namespace legal_work_api.Mappers
{
    public static class JornadaMapper
    {
        public static JornadaEntity ToJornadaEntity(this JornadaDto j)
        {
            return new JornadaEntity()
            {
                DataInicio = j.DataInicio,
                HorasTrabalhadas = j.HorasTrabalhadas,
            };
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace legal_work_api.Models
{
    [Table("lw_jornada")]
    public class JornadaEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Funcionario")]
        public int FuncionarioId { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "O formato deve ser de horas: HH:mm.")]
        public string HorasTrabalhadas { get; set; } = string.Empty;

        public virtual FuncionarioEntity Funcionario { get; set; }
    }
}

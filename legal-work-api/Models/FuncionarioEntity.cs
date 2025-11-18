using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace legal_work_api.Models
{
    [Table("lw_funcionario")]
    public class FuncionarioEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(11, ErrorMessage = "O CPF deve conter 11 dígitos.")]
        public string Cpf { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Endereco { get; set; } = string.Empty;

        [Required]
        [StringLength(11, ErrorMessage = "O número de telefone deve conter até 11 dígitos.")]
        public string Telefone { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        // 🔗 FK para Empresa
        [Required]
        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }

        // 🔗 Navegação
        public virtual EmpresaEntity Empresa { get; set; }

        // 🔗 Relacionamento 1:N com Jornada
        public virtual ICollection<JornadaEntity> Jornadas { get; set; } = new List<JornadaEntity>();
    }
}

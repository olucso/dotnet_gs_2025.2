using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace legal_work_api.Models
{
    [Table("lw_empresa")]
    public class EmpresaEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string RazaoSocial { get; set; } = string.Empty;

        [Required]
        [StringLength(14, ErrorMessage = "O CNPJ deve conter 14 dígitos.")]
        public string Cnpj { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Endereco { get; set; } = string.Empty;

        [Required]
        [StringLength(11, ErrorMessage = "O número de telefone deve conter até 11 dígitos.")]
        public string Telefone { get; set; } = string.Empty;
    }
}

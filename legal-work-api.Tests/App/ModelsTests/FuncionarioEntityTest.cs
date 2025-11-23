using legal_work_api.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace legal_work_api.Tests.App.ModelsTests
{
    public class FuncionarioEntityTest
    {
        // Método auxiliar para validar o objeto e retornar erros
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        // Teste de funcionário válido
        [Fact]
        public void Funcionario_IsValid()
        {
            var funcionario = new FuncionarioEntity
            {
                Id = 1,
                Nome = "Maria Santos",
                Cpf = "12345678901",
                Endereco = "Rua X, 123",
                Telefone = "11987654321",
                Email = "maria.santos@example.com"
            };

            var validationResults = ValidateModel(funcionario);

            Assert.Empty(validationResults);
        }

        // Testes de propriedades obrigatórias (Required) - null
        [Theory]
        [InlineData("Nome")]
        [InlineData("Cpf")]
        [InlineData("Endereco")]
        [InlineData("Telefone")]
        [InlineData("Email")]
        public void Funcionario_RequiredProperties_Null(string propertyName)
        {
            var funcionario = new FuncionarioEntity
            {
                Id = 1,
                Nome = "Maria Santos",
                Cpf = "12345678901",
                Endereco = "Rua X, 123",
                Telefone = "11987654321",
                Email = "maria.santos@example.com"
            };

            typeof(FuncionarioEntity).GetProperty(propertyName)?.SetValue(funcionario, null);

            var validationResults = ValidateModel(funcionario);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("required"));
        }

        // Testes de propriedades obrigatórias (Required) - string vazia
        [Theory]
        [InlineData("Nome")]
        [InlineData("Cpf")]
        [InlineData("Endereco")]
        [InlineData("Telefone")]
        [InlineData("Email")]
        public void Funcionario_RequiredProperties_Empty(string propertyName)
        {
            var funcionario = new FuncionarioEntity
            {
                Id = 1,
                Nome = "Maria Santos",
                Cpf = "12345678901",
                Endereco = "Rua X, 123",
                Telefone = "11987654321",
                Email = "maria.santos@example.com"
            };

            typeof(FuncionarioEntity).GetProperty(propertyName)?.SetValue(funcionario, "");

            var validationResults = ValidateModel(funcionario);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("required"));
        }

        // Testes para limite de comprimento (StringLength)
        [Theory]
        [InlineData("Nome", 201)]
        [InlineData("Cpf", 12)] // Tem erro customizado
        [InlineData("Endereco", 501)]
        [InlineData("Telefone", 12)] // Tem erro customizado
        [InlineData("Email", 201)]
        public void Funcionario_StringLength_Exceeds(string propertyName, int length)
        {
            var funcionario = new FuncionarioEntity
            {
                Id = 1,
                Nome = "Maria Santos",
                Cpf = "12345678901",
                Endereco = "Rua X, 123",
                Telefone = "11987654321",
                Email = "maria.santos@example.com"
            };

            typeof(FuncionarioEntity).GetProperty(propertyName)?.SetValue(funcionario, new string('A', length));

            var validationResults = ValidateModel(funcionario);

            Assert.Contains(validationResults, vr =>
                vr.ErrorMessage.Contains("maximum length") ||
                vr.ErrorMessage.Contains("deve conter"));
        }

        // Id Required - valor default (0) deve gerar erro
        [Fact]
        public void Funcionario_Id_Required()
        {
            var funcionario = new FuncionarioEntity
            {
                Id = 0, // inválido pois é [Required]
                Nome = "Maria Santos",
                Cpf = "12345678901",
                Endereco = "Rua X, 123",
                Telefone = "11987654321",
                Email = "maria.santos@example.com"
            };

            var validationResults = ValidateModel(funcionario);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("required"));
        }
    }
}

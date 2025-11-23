using legal_work_api.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace legal_work_api.Tests.App
{
    public class EmpresaEntityTest
    {
        // Método auxiliar para validar o objeto e retornar erros
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        // Teste de empresa válida
        [Fact]
        public void Empresa_IsValid()
        {
            var empresa = new EmpresaEntity
            {
                Id = 1,
                RazaoSocial = "Empresa Exemplo Ltda",
                Cnpj = "12345678000199",
                Endereco = "Rua Exemplo, 123",
                Telefone = "11999999999"
            };

            var validationResults = ValidateModel(empresa);

            Assert.Empty(validationResults);
        }

        // Testes para propriedades obrigatórias (Required) - null
        [Theory]
        [InlineData("RazaoSocial")]
        [InlineData("Cnpj")]
        [InlineData("Endereco")]
        [InlineData("Telefone")]
        public void Empresa_RequiredProperties_Null(string propertyName)
        {
            var empresa = new EmpresaEntity
            {
                Id = 1,
                RazaoSocial = "Empresa Exemplo",
                Cnpj = "12345678000199",
                Endereco = "Rua Teste",
                Telefone = "11999999999"
            };

            typeof(EmpresaEntity).GetProperty(propertyName)?.SetValue(empresa, null);

            var validationResults = ValidateModel(empresa);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("required"));
        }

        // Testes para propriedades obrigatórias (Required) - empty
        [Theory]
        [InlineData("RazaoSocial")]
        [InlineData("Cnpj")]
        [InlineData("Endereco")]
        [InlineData("Telefone")]
        public void Empresa_RequiredProperties_Empty(string propertyName)
        {
            var empresa = new EmpresaEntity
            {
                Id = 1,
                RazaoSocial = "Empresa Exemplo",
                Cnpj = "12345678000199",
                Endereco = "Rua Teste",
                Telefone = "11999999999"
            };

            typeof(EmpresaEntity).GetProperty(propertyName)?.SetValue(empresa, "");

            var validationResults = ValidateModel(empresa);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("required"));
        }

        // Testes para StringLength - exceder limite
        [Theory]
        [InlineData("RazaoSocial", 201)]       // limite 200
        [InlineData("Endereco", 501)]          // limite 500
        public void Empresa_StringLength_Exceeds(string propertyName, int length)
        {
            var empresa = new EmpresaEntity
            {
                Id = 1,
                RazaoSocial = "Empresa Exemplo",
                Cnpj = "12345678000199",
                Endereco = "Rua Teste",
                Telefone = "11999999999"
            };

            typeof(EmpresaEntity).GetProperty(propertyName)?.SetValue(empresa, new string('A', length));

            var validationResults = ValidateModel(empresa);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("maximum length"));
        }

        // Teste especial para CNPJ excedendo 14 dígitos
        [Fact]
        public void Empresa_Cnpj_ExceedsLength()
        {
            var empresa = new EmpresaEntity
            {
                Id = 1,
                RazaoSocial = "Empresa",
                Cnpj = new string('1', 15), // 15 dígitos
                Endereco = "Rua Teste",
                Telefone = "11999999999"
            };

            var validationResults = ValidateModel(empresa);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("14 dígitos"));
        }

        // Teste especial para Telefone excedendo 11 dígitos
        [Fact]
        public void Empresa_Telefone_ExceedsLength()
        {
            var empresa = new EmpresaEntity
            {
                Id = 1,
                RazaoSocial = "Empresa",
                Cnpj = "12345678000199",
                Endereco = "Rua Teste",
                Telefone = new string('9', 12) // 12 dígitos
            };

            var validationResults = ValidateModel(empresa);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("11 dígitos"));
        }
    }
}

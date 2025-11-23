using legal_work_api.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace legal_work_api.Tests.App
{
    public class JornadaEntityTest
    {
        // Método auxiliar para validar o objeto e retornar erros
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        // Teste de jornada válida
        [Fact]
        public void Jornada_IsValid()
        {
            var jornada = new JornadaEntity
            {
                Id = 1,
                DataInicio = DateTime.Now,
                HorasTrabalhadas = "08:30"
            };

            var validationResults = ValidateModel(jornada);

            Assert.Empty(validationResults);
        }

        // Teste para DataInicio (DateTime, Required) - valor padrão
        [Fact]
        public void Jornada_DataInicio_DefaultValue()
        {
            var jornada = new JornadaEntity
            {
                Id = 1,
                DataInicio = DateTime.MinValue, // Valor padrão
                HorasTrabalhadas = "08:00"
            };

            var validationResults = ValidateModel(jornada);

            // DateTime.MinValue pode ou não ser considerado válido; ajustar conforme necessidade
            Assert.Empty(validationResults);
        }

        // Teste para HorasTrabalhadas (string, Required) - formato válido
        [Fact]
        public void Jornada_HorasTrabalhadas_ValidFormat()
        {
            var jornada = new JornadaEntity
            {
                Id = 1,
                DataInicio = DateTime.Now,
                HorasTrabalhadas = "07:45"
            };

            var validationResults = ValidateModel(jornada);

            Assert.Empty(validationResults);
        }

        // Testes para HorasTrabalhadas com formatos inválidos
        [Theory]
        [InlineData("7:45")]       // Sem zero à esquerda
        [InlineData("0745")]       // Sem dois pontos
        [InlineData("25:00")]      // Hora inválida
        [InlineData("aa:bb")]      // Não numérico
        [InlineData("123456")]     // Muito longo
        [InlineData("")]           // Vazio
        public void Jornada_HorasTrabalhadas_InvalidFormat(string horas)
        {
            var jornada = new JornadaEntity
            {
                Id = 1,
                DataInicio = DateTime.Now,
                HorasTrabalhadas = horas
            };

            var validationResults = ValidateModel(jornada);

            Assert.NotEmpty(validationResults);
        }
    }
}

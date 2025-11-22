using legal_work_api.Data.Repositories.Interfaces;
using legal_work_api.Dtos;
using legal_work_api.Mappers;
using legal_work_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;

namespace legal_work_api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioController(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista de Funcionários",
            Description = "Retorna uma lista completa com todos os funcionários cadastrados.")]
        [SwaggerResponse(statusCode: 200, description: "Lista de funcionários retornada com sucesso.", type: typeof(IEnumerable<FuncionarioEntity>))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Displacement = 0, int TotalRecords = 3)
        {
            var result = await _funcionarioRepository.GetAll(Displacement, TotalRecords);

            if (!result.Data.Any())
                return NoContent();

            var Id = result.Data.FirstOrDefault()?.Id ?? 0;

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Funcionario", null, Request.Scheme),
                    getById = Url.Action(nameof(Get), "Funcionario", new { id = Id }, Request.Scheme),
                    post = Url.Action(nameof(Post), "Funcionario", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Funcionario", new { id = Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Funcionario", new { id = Id }, Request.Scheme),
                },
                page = new
                {
                    result.Deslocamento,
                    result.RegistrosRetornados,
                    result.TotalRegistros
                }
            };

            return Ok(hateoas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Busca de um funcionário apenas, através de seu ID",
            Description = "Retorna um funcionário específico, com base no ID informado.")]
        [SwaggerResponse(statusCode: 200, description: "Funcionário encontrado.", type: typeof(IEnumerable<FuncionarioEntity>))]
        [SwaggerResponse(statusCode: 404, description: "Funcionário não encontrado.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _funcionarioRepository.GetById(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Funcionario", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Funcionario", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Funcionario", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Funcionario", new { id = result.Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Funcionario", new { id = result.Id }, Request.Scheme),
                }
            };

            return Ok(hateoas);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastro de Funcionário",
            Description = "Cadastra um funcionário no banco de dados.")]
        [SwaggerResponse(statusCode: 200, description: "Funcionário cadastrado com sucesso.", type: typeof(FuncionarioEntity))]
        [SwaggerResponse(statusCode: 400, description: "Erro ao cadastrar funcionário.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public IActionResult Post(FuncionarioDto entity)
        {
            try
            {
                var result = _funcionarioRepository.Add(entity.ToFuncionarioEntity());

                var hateoas = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Funcionario", new { id = result.Id }, Request.Scheme),
                        getAll = Url.Action(nameof(Get), "Funcionario", null, Request.Scheme),
                        put = Url.Action(nameof(Put), "Funcionario", new { id = result.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Funcionario", new { id = result.Id }, Request.Scheme)
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Alteração no cadastro do funcionário.",
            Description = "Atualiza o cadastro de um funcionário no banco de dados.")]
        [SwaggerResponse(statusCode: 200, description: "Funcionário atualizado com sucesso.", type: typeof(FuncionarioEntity))]
        [SwaggerResponse(statusCode: 404, description: "Funcionário não encontrado.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Put(int id, FuncionarioDto entity)
        {
            var result = await _funcionarioRepository.Update(id, entity.ToFuncionarioEntity());

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Funcionario", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Funcionario", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Funcionario", null, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Funcionario", new { id = result.Id }, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deleção do cadastro de um funcionário.",
            Description = "Deleta o cadastro de um funcionário a partir de seu ID.")]
        [SwaggerResponse(statusCode: 200, description: "Funcionário deletado com sucesso.")]
        [SwaggerResponse(statusCode: 404, description: "Funcionário não encontrado.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _funcionarioRepository.Delete(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                message = "Funcionário deletado com sucesso.",
                links = new
                {
                    getAll = Url.Action(nameof(Get), "Funcionario", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Funcionario", null, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }
    }
}

using legal_work_api.Data.Repositories.Interfaces;
using legal_work_api.Dtos;
using legal_work_api.Mappers;
using legal_work_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;

namespace legal_work_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaController(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista de Empresas Cadastradas", Description = "Retorna uma lista contendo todas as empresas cadastradas no banco de dados.")]
        [SwaggerResponse(200, "Lista retornada com sucesso.", typeof(IEnumerable<EmpresaEntity>))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Displacement = 0, int TotalRecords = 3)
        {
            var result = await _empresaRepository.GetAll(Displacement, TotalRecords);

            if (!result.Data.Any())
                return NoContent();

            var Id = result.Data.FirstOrDefault()?.Id ?? 0;

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Empresa", null, Request.Scheme),
                    getById = Url.Action(nameof(Get), "Empresa", new { id = Id }, Request.Scheme),
                    post = Url.Action(nameof(Post), "Empresa", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Empresa", new { id = Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Empresa", new { id = Id }, Request.Scheme),
                }
            };

            return Ok(hateoas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca uma empresa apenas, através de seu ID", Description = "Retorna uma empresa específica, com base no ID informado.")]
        [SwaggerResponse(200, "Empresa encontrada.", typeof(EmpresaEntity))]
        [SwaggerResponse(404, "Empresa não encontrada.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _empresaRepository.GetById(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Empresa", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Empresa", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Empresa", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Empresa", new { id = result.Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Empresa", new { id = result.Id }, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastro de Empresa", Description = "Cadastra uma nova empresa no banco de dados.")]
        [SwaggerResponse(200, "Empresa cadastrada com sucesso.", typeof(EmpresaEntity))]
        [SwaggerResponse(400, "Erro ao cadastrar empresa.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Post(EmpresaDto entity)
        {
            try
            {
                var result = await _empresaRepository.Add(entity.ToEmpresaEntity());

                var hateoas = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Empresa", new { id = result.Id }, Request.Scheme),
                        getAll = Url.Action(nameof(Get), "Empresa", null, Request.Scheme),
                        put = Url.Action(nameof(Put), "Empresa", new { id = result.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Empresa", new { id = result.Id }, Request.Scheme)
                    }
                };

                return Ok(hateoas);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Alteração no cadastro da empresa.", Description = "Atualiza o cadastro de uma empresa no banco de dados.")]
        [SwaggerResponse(200, "Empresa atualizada com sucesso.", typeof(EmpresaEntity))]
        [SwaggerResponse(404, "Empresa não encontrada.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Put(int id, EmpresaDto entity)
        {
            var result = await _empresaRepository.Update(id, entity.ToEmpresaEntity());

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Empresa", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Empresa", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Emprrsa", null, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Empresa", new { id = result.Id }, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleção do cadastro de uma empresa.", Description = "Deleta o cadastro de uma empresa a partir de seu ID.")]
        [SwaggerResponse(200, "Empresa deletada com sucesso.")]
        [SwaggerResponse(404, "Empresa não encontrada.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _empresaRepository.Delete(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                message = "Empresa deletada com sucesso.",
                links = new
                {
                    getAll = Url.Action(nameof(Get), "Empresa", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Empresa", null, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }
    }
}

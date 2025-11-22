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
    public class JornadaController : ControllerBase
    {
        private readonly IJornadaRepository _jornadaRepository;

        public JornadaController(IJornadaRepository jornadaRepository)
        {
            _jornadaRepository = jornadaRepository;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista de Jornadas de Trabalho", Description = "Retorna uma lista contendo as jornadas de trabalho cadastradas no banco de dados.")]
        [SwaggerResponse(200, "Lista retornada com sucesso.", typeof(IEnumerable<JornadaEntity>))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Displacement = 0, int TotalRecords = 3)
        {
            var result = await _jornadaRepository.GetAll(Displacement, TotalRecords);

            if (!result.Data.Any())
                return NoContent();

            var Id = result.Data.FirstOrDefault()?.Id ?? 0;

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Jornada", null, Request.Scheme),
                    getById = Url.Action(nameof(Get), "Jornada", new { id = Id }, Request.Scheme),
                    post = Url.Action(nameof(Post), "Jornada", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Jornada", new { id = Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Jornada", new { id = Id }, Request.Scheme),
                }
            };

            return Ok(hateoas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna uma jornada de trabalho apenas, através de seu ID", Description = "Retorna uma jornada de trabalho específica, com base no ID informado.")]
        [SwaggerResponse(200, "Jornada encontrada.", typeof(JornadaEntity))]
        [SwaggerResponse(404, "Jornada não encontrada.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _jornadaRepository.GetById(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Jornada", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Jornada", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Jornada", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Jornada", new { id = result.Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Jornada", new { id = result.Id }, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastro de Jornada de Trabalho", Description = "Cadastra uma nova jornada de trabalho no banco de dados.")]
        [SwaggerResponse(200, "Jornada cadastrada com sucesso.", typeof(JornadaEntity))]
        [SwaggerResponse(400, "Erro ao cadastrar jornada.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Post(JornadaDto entity)
        {
            try
            {
                var result = await _jornadaRepository.Add(entity.ToJornadaEntity());

                var hateoas = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Jornada", new { id = result.Id }, Request.Scheme),
                        getAll = Url.Action(nameof(Get), "Jornada", null, Request.Scheme),
                        put = Url.Action(nameof(Put), "Jornada", new { id = result.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Jornada", new { id = result.Id }, Request.Scheme)
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
        [SwaggerOperation(Summary = "Alteração no cadastro da jornada de trabalho.", Description = "Atualiza o cadastro de uma jornada de trabalho no banco de dados.")]
        [SwaggerResponse(200, "Jornada atualizada com sucesso.", typeof(JornadaEntity))]
        [SwaggerResponse(404, "Jornada não encontrada.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Put(int id, JornadaDto entity)
        {
            var result = await _jornadaRepository.Update(id, entity.ToJornadaEntity());

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Jornada", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Jornada", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Jornada", null, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Jornada", new { id = result.Id }, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleção do cadastro de uma jornada de trabalho.", Description = "Deleta o cadastro de uma jornada de trabalho a partir de seu ID.")]
        [SwaggerResponse(200, "Jornada deletada com sucesso.")]
        [SwaggerResponse(404, "Jornada não encontrada.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _jornadaRepository.Delete(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                message = "Jornada deletada com sucesso.",
                links = new
                {
                    getAll = Url.Action(nameof(Get), "Jornada", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Jornada", null, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }
    }
}

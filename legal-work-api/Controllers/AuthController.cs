using legal_work_api.Dtos;
using legal_work_api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace legal_work_api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Autenticação de usuário",
            Description = "Realiza login e retorna um token JWT válido.")]
        [SwaggerResponse(statusCode: 200, description: "Login bem sucedido.")]
        [SwaggerResponse(statusCode: 401, description: "Credenciais inválidas.")]
        public IActionResult Login([FromBody] UserLoginDto login)
        {
            if (login.Username == "admin" && login.Password == "123456")
            {
                var token = _jwtService.GenerateToken(login.Username);

                return Ok(new
                {
                    user = login.Username,
                    token,
                    expiresIn = 60 * 60,
                    type = "Bearer"
                });
            }

            return Unauthorized(new { message = "Usuário ou senha inválidos." });
        }
    }
}

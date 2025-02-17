using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GestaoDeUsuario.Application.DTO;
using GestaoDeUsuario.Application.Interfaces;
using System.Threading.Tasks;

namespace GestaoDeUsuario.Api.Controller;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [AllowAnonymous]
    [HttpPost("/CadastrarUsuario")]
    public async Task<IActionResult> Cadastrar([FromBody] CadastrarUsuarioDto request)
    {
        try
        {
            UsuarioDTO usuario = await _usuarioService.CadastrarUsuario(request);
            if (usuario == null)
                return Conflict(new { message = "Login já cadastrado." });

            return Ok(new { message = "Usuário cadastrado com sucesso." });

        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro interno do servidor: " + ex.Message);
        }
    }

    [HttpGet("/GetUsuarios")]
    public async Task<ActionResult<IEnumerable<UsuarioDTO>>> RetornaListaUsuarios()
    {
        IEnumerable<UsuarioDTO> usuarios = _usuarioService.RetornaListaUsuarios();
        return Ok(usuarios);
    }

    [HttpPut("/AlterarDadosUsuario/{id}")]
    public async Task<IActionResult> AlterarDadosUsuario([FromBody] UsuarioDTO usuarioDTO, int id)
    {
        try
        {
            var resultado = await _usuarioService.AtualizarDadosUsuario(id, usuarioDTO);

            if (resultado)
            {
                return Ok(new { message = "Usuário atualizado com sucesso!" });
            }

            return NotFound(new { message = "Usuário não encontrado" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Erro ao atualizar usuário: {ex.Message}" });
        }
    }

    [HttpDelete("/DeletarUsuario/{id}")]
    public async Task<IActionResult> DeletarUsuario(int id)
    {
        try
        {
            await _usuarioService.DeletarUsuario(id);
            return Ok(new { message = "Usuário deletado com sucesso!" });

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Erro ao deletar usuário: {ex.Message}" });
        }
    }


}

using GestaoDeUsuario.Application.DTO;
using GestaoDeUsuario.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeUsuario.Api.Controller;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUsuarioService _usuarioService;

    public AuthenticationController(ITokenService tokenServie, IUsuarioService usuarioService)
    {
        _tokenService = tokenServie;
        _usuarioService = usuarioService;
    }

    [HttpPost("/Autenticar")]
    public async Task<IActionResult> Autenticar([FromBody] LoginDTO command)
    {
        // Verificando se o usuário existe
        var usuario = await _usuarioService.BuscarUsuario(command.Login, command.Senha);
        if (usuario == null)
            return NotFound(new{message = "Usuário não encontrado. Verifique o login e senha."});

        // Criando o token para o usuário
        TokenDto tokenDto = await _tokenService.CreateToken(usuario);
        if (tokenDto == null)
            return UnprocessableEntity(new{message = "Erro ao gerar token."});

        return Ok(tokenDto);
    }


    [HttpPost("/RefreshToken")]
    public IActionResult RefreshToken([FromBody] RefreshTokenDto request)
    {
        TokenDto tokenDto = _tokenService.RefreshToken(request.Token, request.TokenRefresh);
        if (tokenDto == null)
            return UnprocessableEntity(new{message = "Erro ao atualizar token."});

        return Ok(new{message = tokenDto});
    }
}

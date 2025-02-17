using GestaoDeUsuario.Application.DTO;

namespace GestaoDeUsuario.Application.Interfaces;

public interface ITokenService
{
    Task<TokenDto> CreateToken(UsuarioDTO usuarioDTO);
    TokenDto RefreshToken(string oldToken, string refreshToken);
}

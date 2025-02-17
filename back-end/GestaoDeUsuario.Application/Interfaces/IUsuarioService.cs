using GestaoDeUsuario.Application.DTO;

namespace GestaoDeUsuario.Application.Interfaces;

public interface IUsuarioService
{
    IEnumerable<UsuarioDTO> RetornaListaUsuarios();
    Task<UsuarioDTO> BuscarUsuario(string login, string senha);
    Task<UsuarioDTO> CadastrarUsuario(CadastrarUsuarioDto request);
    Task<bool> AtualizarDadosUsuario(int id, UsuarioDTO usuarioDTO);
    Task<bool> DeletarUsuario(int id);
    bool VerificaSeUsuarioExiste(string login, string nomeUsuario);
}

using System;
using GestaoDeUsuario.Domain.Entities;

namespace GestaoDeUsuario.Domain.Interfaces;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    public Task<Usuario> BuscarUsuario(string login, string senha);
    public bool ValidaSeUsuarioExiste(string login, string usuarioNome);
    public Task<bool> DeletarUsuario(Usuario usuario);
    public Task<bool> AtualizarDadosUsuario(Usuario usuario);
    Task<Usuario> ListarTodosUsuarios(Usuario usuario);
    Task<Usuario> BuscarUsuarioPorLogin(string login);
    Task<Usuario> BuscarUsuarioPorId(int id);
}

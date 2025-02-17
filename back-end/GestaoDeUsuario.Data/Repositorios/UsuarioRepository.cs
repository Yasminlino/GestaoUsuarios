using GestaoDeUsuario.Domain.Interfaces;
using GestaoDeUsuario.Data.Context;
using GestaoDeUsuario.Domain.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeUsuario.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ContextDB _context;

        public UsuarioRepository(ContextDB context)
        {
            _context = context;
        }

        public void AlterarStatusAtivo(int codigoId, bool statusAtivo)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Usuario entidade, int codigoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> BuscarPorExpressao(Expression<Func<Usuario, bool>> filtro)
        {
            throw new NotImplementedException();
        }

        public Usuario BuscarPorId(int codigoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> BuscarTodos()
        {
            return _context.Usuarios.ToList().OrderBy(u => u.Id);
        }
        public void Deletar(int codigoId)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> BuscarUsuario(string login, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UserName == login);

            return usuario;
        }

        public async Task Cadastrar(Usuario entidade)
        {
            _context.Usuarios.Add(entidade);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletarUsuario(Usuario usuario)
        {

            _context.Usuarios.Remove(usuario);
            var resultado = await _context.SaveChangesAsync();
            if (resultado > 0) 
            {
                Console.WriteLine("Usuário deletado com sucesso.");
                return true;
            }
            else
            {
                Console.WriteLine("Erro ao deletar usuário, nenhuma linha foi afetada.");
                return false;
            }
        }

        public bool ValidaSeUsuarioExiste(string login, string usuarioNome)
        {
            return _context.Usuarios
                .Any(u => u.UserName == login || u.NomeUsuario == usuarioNome);
        }

        public async Task<bool> AtualizarDadosUsuario(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public Task<Usuario> ListarTodosUsuarios(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> BuscarUsuarioPorLogin(string login)
        {
            var usuario = await _context.Usuarios
                             .FirstOrDefaultAsync(u => u.UserName == login);
            return usuario;
        }

        public async Task<Usuario> BuscarUsuarioPorId(int id)
        {
            var usuario = await _context.Usuarios
                             .FirstOrDefaultAsync(u => u.Id == id);
            return usuario;
        }
    }
}

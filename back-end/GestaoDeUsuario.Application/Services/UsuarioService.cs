using System;
using GestaoDeUsuario.Application.DTO;
using GestaoDeUsuario.Application.Interfaces;
using GestaoDeUsuario.Domain.Entities;
using GestaoDeUsuario.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using GestaoDeUsuario.Application.Utils;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace GestaoDeUsuario.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordHasher<Usuario> _passwordHasher;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = new PasswordHasher<Usuario>();
            _mapper = mapper;
        }

        public async Task<bool> AtualizarDadosUsuario(int id, UsuarioDTO usuarioDTO)
        {
            var usuario = await _usuarioRepository.BuscarUsuarioPorId(id);

            if (usuario == null)
                return false;

            usuario.UserName = usuarioDTO.Login;
            usuario.NomeUsuario = usuarioDTO.NomeUsuario;
            usuario.Role = usuarioDTO.Role;

            if (usuarioDTO.Senha != "null")
            {
                usuario.SetSenhaHash(usuarioDTO.Senha);
            }

            await _usuarioRepository.AtualizarDadosUsuario(usuario);

            return true;
        }


        public async Task<UsuarioDTO> BuscarUsuario(string login, string senha)
        {
            Usuario usuario = await _usuarioRepository.BuscarUsuario(login, senha);

            if (usuario == null)
                return null;

            var resultadoSenha = _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, senha);

            if (resultadoSenha != PasswordVerificationResult.Success)
                return null;

            return new UsuarioDTO(usuario.UserName, usuario.Role, usuario.NomeUsuario);
        }

        public async Task<UsuarioDTO> CadastrarUsuario(CadastrarUsuarioDto request)
        {
            try
            {
                var validaLogin = await _usuarioRepository.BuscarUsuarioPorLogin(request.Login);

                if (validaLogin != null)
                {
                    throw new InvalidOperationException("Login já cadastrado.");
                }

                var usuarios = _usuarioRepository.BuscarTodos().ToList();
                int ultimoCodigoIdCadastrado = usuarios.Any() ? _usuarioRepository.BuscarTodos().Max(x => x.Id) : 0;

                Usuario usuario = new Usuario(
                    request.Login,
                    request.Senha,
                    request.NomeUsuario,
                    request.Role,
                    true
                );

                await _usuarioRepository.Cadastrar(usuario);

                return new UsuarioDTO(request.Login, request.Senha, request.NomeUsuario, request.Role);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar usuario: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return null;
            }
        }


        public async Task<bool> DeletarUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.BuscarUsuarioPorId(id);

                if (usuario == null)
                    throw new Exception("Usuario não encontrado!");
                if (usuario.Id == 1)
                    throw new Exception("Não é possivel deletar o usuario principal!");

                await _usuarioRepository.DeletarUsuario(usuario);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar o usuário: {ex.Message}");
                throw;
            }
        }


        public IEnumerable<UsuarioDTO> RetornaListaUsuarios()
        {
            var usuarios = _usuarioRepository.BuscarTodos();
            var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return usuariosDTO;
        }


        public bool VerificaSeUsuarioExiste(string login, string nomeUsuario)
        {
            return _usuarioRepository.ValidaSeUsuarioExiste(login, nomeUsuario);
        }

    }
}

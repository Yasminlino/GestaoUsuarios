using Microsoft.AspNetCore.Identity;
using System;

namespace GestaoDeUsuario.Domain.Entities
{
    public class Usuario : IdentityUser<int>
    {
        public string NomeUsuario { get; set; }
        public bool StatusAtivo { get; set; }
        public string Role { get; set; }
        public Usuario()
        {
        }

        // Construtor da classe Usuario
        public Usuario(string userName,
               string senha,
               string nomeUsuario,
               string role,
               bool statusAtivo)
        {
            this.UserName = userName;
            this.Email = userName;
            this.NomeUsuario = nomeUsuario;
            this.Role = role;
            this.StatusAtivo = statusAtivo;

            SetSenhaHash(senha);
        }

        public void SetSenhaHash(string senha)
        {
            var passwordHasher = new PasswordHasher<Usuario>();
            this.PasswordHash = passwordHasher.HashPassword(this, senha);
        }
    }
}

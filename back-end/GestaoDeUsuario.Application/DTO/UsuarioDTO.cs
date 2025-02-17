using System.ComponentModel.DataAnnotations;
namespace GestaoDeUsuario.Application.DTO;

public class UsuarioDTO
{
    public int Id { get; set; }
    [Required]
    public string Login { get; set; }
    public string Senha { get; set; }
    public string NomeUsuario { get; set; }
    public string Role { get; set; }

    public UsuarioDTO() { }
    public UsuarioDTO(string login, string senha) {
        Login = login;
        Senha = senha;
    }

    public UsuarioDTO(string? login, string role, string nomeUsuario) {
        Login = login;
        NomeUsuario = nomeUsuario;
        Role = role;
    }

    public UsuarioDTO(string login, string? senha, string nomeUsuario, string role)
    {
        Login = login;
        Senha = senha;
        NomeUsuario = nomeUsuario;
        Role = role;
    }
    public UsuarioDTO(int id,string login, string? senha, string nomeUsuario, string role)
    {
        Id = id;
        Login = login;
        Senha = senha;
        NomeUsuario = nomeUsuario;
        Role = role;
    }
}
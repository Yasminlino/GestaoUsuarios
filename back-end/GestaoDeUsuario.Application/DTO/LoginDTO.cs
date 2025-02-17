using System.ComponentModel.DataAnnotations;
namespace GestaoDeUsuario.Application.DTO;

public class LoginDTO
{
    [Required]
    public string Login { get; set; }

    [Required]
    public string Senha { get; set; }
    public LoginDTO() { }
    public LoginDTO(string login, string senha) {
        Login = login;
        Senha = senha;
    }

}
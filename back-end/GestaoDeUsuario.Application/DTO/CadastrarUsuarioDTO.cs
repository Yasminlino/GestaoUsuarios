namespace GestaoDeUsuario.Application.DTO;

public sealed record CadastrarUsuarioDto(string Login, string Senha, string NomeUsuario, string Role);
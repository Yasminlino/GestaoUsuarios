namespace GestaoDeUsuario.Application.DTO;

public sealed record TokenDto(object Token, object TokenRefresh, DateTime DataCriacao, DateTime DataExpiracao);

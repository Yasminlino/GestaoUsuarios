namespace GestaoDeUsuario.Application.Interfaces;

public interface IValidadorTokenService
{
    public bool ValidarTokenPorUsuario(string token);
}

using System;

namespace GestaoDeUsuario.Domain.Interfaces;

public interface IAuthenticate
{
    bool Authenticate(string email, string password);
    Task<bool> RegisterUser(string email, string password);
    Task Logout();
}

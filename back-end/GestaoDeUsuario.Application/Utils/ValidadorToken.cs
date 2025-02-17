using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using GestaoDeUsuario.Application.Interfaces;

namespace GestaoDeUsuario.Application.Utils;

public class ValidadorToken(IUsuarioService usuarioService) : IValidadorTokenService
{
    private readonly IUsuarioService _usuarioService = usuarioService;

    public bool ValidarTokenPorUsuario(string token)
    {
        if (token is null) return false;

        JwtSecurityTokenHandler handler = new();
        List<Claim> claims = handler.ReadJwtToken(token).Payload.Claims.ToList();
        if(claims is null) throw new SecurityTokenException("Token inválido: claims não encontradas");

        Claim givenName = claims.FirstOrDefault(c => c.Type == "given_name") ?? throw new SecurityTokenException($"Token inválido: claim 'given_name' não encontrado");
        Claim upn = claims.FirstOrDefault(c => c.Type == "upn") ?? throw new SecurityTokenException("Token inválido: claim 'upn' não encontrado");

        string login = givenName.Value;
        string nomeUsuario = upn.Value;

        if (login is null || nomeUsuario is null) return false;
        return _usuarioService.VerificaSeUsuarioExiste(login, nomeUsuario);
    }
}

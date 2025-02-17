using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestaoDeUsuario.Domain.Entities;

namespace GestaoDeUsuario.Data.Context;
public class ContextDB : IdentityDbContext<Usuario, IdentityRole<int>, int>
{
    public DbSet<Usuario> Usuarios { get; set; }

    public ContextDB(DbContextOptions<ContextDB> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var usuarioAdministrador = new Usuario
        {
            Id = 1,
            UserName = "SisandAdm",
            NormalizedUserName = "SISANDADM",
            NomeUsuario = "Perfil Principal",
            Role = "Admin"
        };

        var hasher = new PasswordHasher<Usuario>();
        usuarioAdministrador.PasswordHash = hasher.HashPassword(usuarioAdministrador, "Sis@nd2025!*");

        builder.Entity<Usuario>().HasData(usuarioAdministrador);
    }

}

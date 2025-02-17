using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GestaoDeUsuario.Data.Context;
using GestaoDeUsuario.Application.Interfaces;
using GestaoDeUsuario.Application.Services;
using GestaoDeUsuario.Application.Utils;
using GestaoDeUsuario.Domain.Interfaces;
using GestaoDeUsuario.Data.Repositories;
using GestaoDeUsuario.Application.Mapping;

namespace GestaoDeUsuario.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterService(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do Entity Framework Core com PostgreSQL
            services.AddDbContext<ContextDB>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            // Configuração da política CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            // Registrar os serviços
            services.AddScoped<IValidadorTokenService, ValidadorToken>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITokenService, TokenService>();

            // Registro do repositório IUsuarioRepository
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Configuração do AutoMapper
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }

}

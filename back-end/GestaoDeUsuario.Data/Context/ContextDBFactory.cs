using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using GestaoDeUsuario.Data.Context;
using System.IO;

namespace GestaoDeUsuario.Data.Context;
public class ContextDBFactory : IDesignTimeDbContextFactory<ContextDB>
{
    public ContextDB CreateDbContext(string[] args)
    {
        var diretorioAtual = Path.Combine(Directory.GetCurrentDirectory(), "..", "GestaoDeUsuario.Api");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(diretorioAtual)
            .AddJsonFile("appsettings.json")  
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ContextDB>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));  

        return new ContextDB(optionsBuilder.Options);
    }
}

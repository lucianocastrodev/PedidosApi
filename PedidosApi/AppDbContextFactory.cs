using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PedidosApi.Data;

namespace PedidosApi;

public class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseSqlite("Data Source=pedidos.db");

        return new AppDbContext(options.Options);
    }
}
using Microsoft.EntityFrameworkCore;
using PedidosApi.Models;

namespace PedidosApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Pedido> Pedidos { get; set; }
}
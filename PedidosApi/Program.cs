using Microsoft.EntityFrameworkCore;
using PedidosApi.Data;
using PedidosApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Adiciona controllers
builder.Services.AddControllers();

// 🔹 Configura CORS para permitir qualquer origem, método e header
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// 🔹 Configura o DbContext para SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=pedidos.db"));

// 🔹 Adiciona SignalR
builder.Services.AddSignalR();


var app = builder.Build();


// 🔹 Usa CORS
app.UseCors("AllowAll");

// 🔹 Map controllers
app.MapControllers();

// 🔹 Mapeia o Hub de pedidos
app.MapHub<PedidoHub>("/pedidoHub");

// 🔹 Inicializa a aplicação
app.Run();
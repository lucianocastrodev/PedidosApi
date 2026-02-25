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

// 🔹 Configura Kestrel para aceitar conexões de qualquer IP na rede
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5220); // escuta todas as interfaces na porta 5220
});

var app = builder.Build();

app.Urls.Add("http://localhost:5220");
app.Urls.Add("http://192.168.1.115:5220"); // substitua pelo IP do seu PC

// 🔹 Usa CORS
app.UseCors("AllowAll");

// 🔹 Map controllers
app.MapControllers();

// 🔹 Mapeia o Hub de pedidos
app.MapHub<PedidoHub>("/pedidoHub");

// 🔹 Inicializa a aplicação
app.Run();
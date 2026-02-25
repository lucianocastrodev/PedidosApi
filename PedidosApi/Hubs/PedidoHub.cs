using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace PedidosApi.Hubs;

[AllowAnonymous] // ✅ Permite que qualquer cliente se conecte
public class PedidoHub : Hub
{
}
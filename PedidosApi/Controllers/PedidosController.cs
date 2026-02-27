using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PedidosApi.Data;
using PedidosApi.Hubs;
using PedidosApi.Models;

[ApiController]
[Route("v1")]
public class PedidosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHubContext<PedidoHub> _hub;

    public PedidosController(AppDbContext context, IHubContext<PedidoHub> hub)
    {
        _context = context;
        _hub = hub;
    }

    [HttpGet("pedidos")]
    public async Task<IActionResult> GetTodos() =>
        Ok(await _context.Pedidos.AsNoTracking().ToListAsync());

    [HttpPost("pedidos")]
    public async Task<IActionResult> Post(Pedido pedido)
    {
        pedido.CriadoEm = DateTime.Now;
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        // Dispara evento SignalR
        await _hub.Clients.All.SendAsync("PedidoCriado", pedido);

        return CreatedAtAction(nameof(GetTodos), new { pedido.Id }, pedido);
    }

    // Atualiza apenas os campos editáveis
    [HttpPut("pedidos/{id}")]
    public async Task<IActionResult> Put(int id, Pedido pedido)
    {
        var p = await _context.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
        if (p == null) return NotFound();

        p.Cliente = pedido.Cliente;
        p.Produto = pedido.Produto;
        p.Quantidade = pedido.Quantidade;
        p.Status = pedido.Status;

        await _context.SaveChangesAsync();

        // Dispara evento SignalR
        await _hub.Clients.All.SendAsync("PedidoAtualizado", p);

        return Ok(p);
    }

    [HttpDelete("pedidos/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await _context.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
        if (p == null) return NotFound();

        _context.Pedidos.Remove(p);
        await _context.SaveChangesAsync();

        // Dispara evento SignalR
        await _hub.Clients.All.SendAsync("PedidoDeletado", id);

        return Ok();
    }
}
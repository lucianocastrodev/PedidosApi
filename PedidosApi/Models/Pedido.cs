namespace PedidosApi.Models;

public class Pedido
{
    public int Id { get; set; }
    public string Cliente { get; set; }
    public string Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal Valor { get; set; }
    public string Status { get; set; } = "Novo";
    public DateTime CriadoEm { get; set; } = DateTime.Now;

    public string { get; set; } = "Tudo certo!";
}
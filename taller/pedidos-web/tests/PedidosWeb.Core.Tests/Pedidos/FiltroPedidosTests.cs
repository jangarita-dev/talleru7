using PedidosWeb.Core.Pedidos;

namespace PedidosWeb.Core.Tests.Pedidos;

public class FiltroPedidosTests
{
    private static readonly DateOnly Desde = new(2026, 3, 1);
    private static readonly DateOnly Hasta = new(2026, 3, 31);

    private static Pedido PedidoDel(int id, DateOnly fecha) =>
        new(id, $"PED-STGO-{id:D6}", "C-001", "Cliente de prueba", fecha, "Emitido", 10000m, "STGO");

    // CA-01
    [Fact]
    public void EntreFechas_PedidoConFechaIgualADesde_Aparece()
    {
        var pedidos = new[] { PedidoDel(1, Desde) };

        var resultado = FiltroPedidos.EntreFechas(pedidos, Desde, Hasta);

        Assert.Equal([1], resultado.Select(p => p.Id));
    }

    // CA-02
    [Fact]
    public void EntreFechas_PedidoConFechaIgualAHasta_Aparece()
    {
        var pedidos = new[] { PedidoDel(1, Hasta) };

        var resultado = FiltroPedidos.EntreFechas(pedidos, Desde, Hasta);

        Assert.Equal([1], resultado.Select(p => p.Id));
    }

    // CA-03: el día anterior a desde y el día siguiente a hasta quedan fuera
    [Fact]
    public void EntreFechas_PedidosFueraDelRango_NoAparecen()
    {
        var pedidos = new[]
        {
            PedidoDel(1, Desde.AddDays(-1)),
            PedidoDel(2, new DateOnly(2026, 3, 15)),
            PedidoDel(3, Hasta.AddDays(1)),
        };

        var resultado = FiltroPedidos.EntreFechas(pedidos, Desde, Hasta);

        Assert.Equal([2], resultado.Select(p => p.Id));
    }

    // CA-04
    [Fact]
    public void EntreFechas_DesdeMayorQueHasta_LanzaArgumentExceptionConMensajeClaro()
    {
        var pedidos = new[] { PedidoDel(1, Desde) };

        var ex = Assert.Throws<ArgumentException>(
            () => FiltroPedidos.EntreFechas(pedidos, Hasta, Desde));

        Assert.Contains("'desde' (2026-03-31) es mayor que 'hasta' (2026-03-01)", ex.Message);
    }

    // Borde de CA-04: un rango de un solo día es válido
    [Fact]
    public void EntreFechas_DesdeIgualAHasta_DevuelveLosPedidosDeEseDia()
    {
        var pedidos = new[] { PedidoDel(1, Desde), PedidoDel(2, Desde.AddDays(1)) };

        var resultado = FiltroPedidos.EntreFechas(pedidos, Desde, Desde);

        Assert.Equal([1], resultado.Select(p => p.Id));
    }

    [Fact]
    public void EntreFechas_ListaNull_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => FiltroPedidos.EntreFechas(null!, Desde, Hasta));
    }
}

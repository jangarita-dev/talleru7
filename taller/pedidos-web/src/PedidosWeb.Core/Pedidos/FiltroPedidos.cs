namespace PedidosWeb.Core.Pedidos;

/// <summary>
/// Filtros sobre listas de pedidos (HU-03).
/// </summary>
public static class FiltroPedidos
{
    /// <summary>
    /// Devuelve los pedidos cuya <see cref="Pedido.Fecha"/> está entre <paramref name="desde"/>
    /// y <paramref name="hasta"/>, ambos inclusive, en el mismo orden en que vienen.
    /// </summary>
    /// <exception cref="ArgumentNullException">Si <paramref name="pedidos"/> es null.</exception>
    /// <exception cref="ArgumentException">Si <paramref name="desde"/> es mayor que <paramref name="hasta"/>.</exception>
    public static IReadOnlyList<Pedido> EntreFechas(IEnumerable<Pedido> pedidos, DateOnly desde, DateOnly hasta)
    {
        ArgumentNullException.ThrowIfNull(pedidos);

        if (desde > hasta)
        {
            throw new ArgumentException(
                $"Rango de fechas inválido: 'desde' ({desde:yyyy-MM-dd}) es mayor que 'hasta' ({hasta:yyyy-MM-dd}).",
                nameof(desde));
        }

        return pedidos.Where(p => p.Fecha >= desde && p.Fecha <= hasta).ToList();
    }
}

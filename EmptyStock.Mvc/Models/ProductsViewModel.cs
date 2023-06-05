using EmptyStock.Domain.Models.Stock;

namespace EmptyStock.Mvc.Models;

public class ProductsViewModel
{
    public IEnumerable<Product> Products { get; set; }
    public IEnumerable<Shipment> Shipments { get; set; }
    public IEnumerable<Receipt> Receipts { get; set; }
}

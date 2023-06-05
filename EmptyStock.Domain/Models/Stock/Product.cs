using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyStock.Domain.Models.Stock;
public class Product : DbEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ICollection<ProductAction> ProductActions { get; set; } = new List<ProductAction>();
    public ICollection<Receipt> Receipts{ get; set; } = new List<Receipt>();
    public ICollection<Shipment> Shipments{ get; set; } = new List<Shipment>();

}

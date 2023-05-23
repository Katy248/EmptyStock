using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyStock.Domain.Models.Identity;

namespace EmptyStock.Domain.Models.Stock;
public abstract class ProductAction : DbEntity
{
    public virtual int ChangeAmount { get; set; }
    public DateTime CreationDate { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int CreatorId { get; set; }
    public StockUser? Creator  { get; set; }
}

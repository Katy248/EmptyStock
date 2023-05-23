using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyStock.Domain.Models.Identity;

namespace EmptyStock.Domain.Models.Stock;
public class Request : DbEntity
{
    public int ProductId { get; set; }
    public int CreatorId { get; set; }
    public decimal Cost { get; set; }
    public int Count { get; set; }
    public Product Product { get; set; }
    public StockUser Creator { get; set; }
}

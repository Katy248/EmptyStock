using EmptyStock.Domain.Models.Identity;
using EmptyStock.Domain.Models.Stock;
using Microsoft.AspNetCore.Mvc;

namespace EmptyStock.Mvc.Models;

public class CreateBillViewModel
{
    public ProductAction ProductAction{ get; set; }
    public StockUser User { get; set; }
}

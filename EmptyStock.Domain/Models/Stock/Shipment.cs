using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyStock.Domain.Models.Stock;
public class Shipment : ProductAction
{
    public int RequestId { get; set; }
    public Request? Request { get; set; }
    public override int ChangeAmount
    {
        get
        {
            return -Request?.Count ?? 0;
        }
        set
        {
            if (Request is not null)
                Request.Count = -value;
        }
    }
}

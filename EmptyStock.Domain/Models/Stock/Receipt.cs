using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyStock.Domain.Models.Stock;
public class Receipt : ProductAction
{
    public int Count { get; set; }
    public override int ChangeAmount
    {
        get
        {
            return Count;
        }
        set
        {
            Count = value;
        }
    }
}

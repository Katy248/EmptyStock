using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EmptyStock.Domain.Models.Identity;
public class StockUser : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
}

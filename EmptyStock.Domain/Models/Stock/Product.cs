﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyStock.Domain.Models.Stock;
public class Product : DbEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

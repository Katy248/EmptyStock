using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyStock.Domain;
public abstract class DbEntity
{
    [Key]
    public int Id { get; set; }
}

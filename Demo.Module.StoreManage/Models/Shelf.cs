using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Module.StoreManage.Models
{
    public class Shelf
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? ShelfName { get; set; }
    }
}

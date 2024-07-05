using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TranDinhBien_BanHang.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}
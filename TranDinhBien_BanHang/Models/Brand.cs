using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TranDinhBien_BanHang.Models
{
    public class Brand
    {
        [Key, Column(Order =0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string BrandAvatar { get; set; }
    }
}
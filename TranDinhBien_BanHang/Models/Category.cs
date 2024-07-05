using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TranDinhBien_BanHang.Models
{
    public class Category
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int CategoryID { get; set; }
        [Required(ErrorMessage ="Vui nhập vào tên thể loại")]
        public string CategoryName { get; set; }
        public string CategoryAvatar { get; set; }
    }
}
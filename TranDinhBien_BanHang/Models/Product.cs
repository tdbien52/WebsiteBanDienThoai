using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TranDinhBien_BanHang.Models
{
    public class Product
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập vào tên sản phẩm!")]
        [RegularExpression("^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ 0-9-]*$", 
            ErrorMessage ="Tên sản phẩm không chứ kí tự đặt biệt")]
        [MinLength(5,ErrorMessage ="Tên sản phẩm phải có ít nhất 5 ký tự")]
        public string ProductName { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập vào ảnh sản phẩm!")]        
        public string Avatar { get; set; }

        [Required(ErrorMessage ="Vui lòng chọn mã thể loại sản phẩm!")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage ="Vui lòng chọn mã thương hiệu!")]
        public int BrandID { get; set; }

        public string ShortDes { get; set; }
        public string FullDescription { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập vào giá sản phẩm")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm không được nhỏ hơn 0")]
        public double Price { get; set; }
        public double PriceDiscount { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<System.DateTime> UpdateAt { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào loại sản phẩm")]
        [RegularExpression("^(0|1|2)$", ErrorMessage = "Loại sản phẩm phải là số 0, 1 hoặc 2")]
        [Range(0, 2, ErrorMessage = "Loại sản phẩm không nằm trong khoảng 0 và 2!")]
        public int Type { get; set; }

        [Required(ErrorMessage ="Vui lòng chọn trạng thái của sản phẩm")]
        public string Status { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}
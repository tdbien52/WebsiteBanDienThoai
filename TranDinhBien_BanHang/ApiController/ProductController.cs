using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TranDinhBien_BanHang.Models;

namespace TranDinhBien_BanHang.ApiController
{
    public class ProductController :System.Web.Http.ApiController
    {
        public List<Product> GetProducts()
        {
            DBConn db = new DBConn ();
            List<Product> products= db.Products.ToList();
            return products;
        }
    }
}

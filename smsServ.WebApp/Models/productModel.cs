using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smsServ.WebApp.Models
{
    public class productModel
    {
        public virtual bool isSuccess { get; set; }
        public virtual string resultMessage { get; set; }
        public virtual List<Product> products { get; set; }
    }

    public class Product
    {
        public virtual string productCode { get; set; }
        public virtual string productName { get; set; }
    }
}

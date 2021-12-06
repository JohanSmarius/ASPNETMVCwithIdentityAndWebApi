using Common.Domain;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class ProductForCustomerViewModel
    {
        public string CustomerName { get; set; }

        public List<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDemoApp.DAL.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string ProductDisplayImage { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string ModelName { get; set; }
        public string Warranty { get; set; }
        public string LaunchYear { get; set; }
        public string Color { get; set; }

        public string CategoryName { get; set; }
        public List<string> ProductImages { get; set; }
        public int Qty { get; set; }
        public decimal TotalAmount { get; set; }

    }
}

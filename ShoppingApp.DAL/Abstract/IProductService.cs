using ShoppingDemoApp.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDemoApp.DAL.Abstract
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByKey(int id);

        Task<List<ProductDto>> GetAllProducts();
    }
}

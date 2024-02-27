using ShoppingDemoApp.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingDemoApp.DAL.Dto;
using ShoppingDemoApp.DAL.Helper;
using Dapper;
using System.Data;
using System.Collections;

namespace ShoppingDemoApp.DAL.Service
{
    public class ProductService : BaseRepository, IProductService
    {
        public ProductService()
        {

        }
        public async Task<List<ProductDto>> GetAllProducts()
        {
            using (connection = Get_Connection())
            {
                var dataObj = await connection.QueryAsync<ProductDto>("GetAllProducts_sp", null, commandType: CommandType.StoredProcedure);
                return dataObj.ToList();
            }
        }

        public async Task<ProductDto> GetProductByKey(int id)
        {
            ProductDto product = new ProductDto();
            using (connection = Get_Connection())
            {
                var param = new DynamicParameters();
                param.Add("@productId", id, DbType.Int64, ParameterDirection.Input);
                ArrayList retObj = new ArrayList();
                using (var questionList = await connection.QueryMultipleAsync("GetProductByKey_sp", param, commandType: CommandType.StoredProcedure))
                {
                    product = questionList.Read<ProductDto>().FirstOrDefault();
                    var prodImages = questionList.Read<string>().ToList();
                    if(product != null)
                    {
                        product.ProductImages = prodImages;
                    }
                }
                return product;
            }
        }
    }
}

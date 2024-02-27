using ShoppingDemoApp.DAL.Abstract;
using ShoppingDemoApp.DAL.Dto;
using ShoppingDemoApp.DAL.Service;
using ShoppingDemoApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoppingDemoApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ActionResult> ProductList()
        {
            var prodList = await _productService.GetAllProducts();
            var model = prodList.Select(x => new ProductViewModel {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Color = x.Color,
                Description = x.Description,
                IsActive = x.IsActive,
                LaunchYear = x.LaunchYear,
                ModelName = x.ModelName,
                Price = x.Price,
                ProductDisplayImage = x.ProductDisplayImage,
                ProductId = x.ProductId,
                ProductImages = x.ProductImages,
                ProductName = x.ProductName,
                Qty = x.Qty,
                TotalAmount = x.TotalAmount,
                Warranty = x.Warranty
            }).ToList();
            return View(model);
        }
        public async Task<ActionResult> ProductDetails(int productId = 0)
        {
            var product = await _productService.GetProductByKey(productId);
            if (product == null) {
                return View(new ProductViewModel());
            }
            var model =  new ProductViewModel
            {
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName,
                Color = product.Color,
                Description = product.Description,
                IsActive = product.IsActive,
                LaunchYear = product.LaunchYear,
                ModelName = product.ModelName,
                Price = product.Price,
                ProductDisplayImage = product.ProductDisplayImage,
                ProductId = product.ProductId,
                ProductImages = product.ProductImages,
                ProductName = product.ProductName,
                Qty = product.Qty,
                TotalAmount = product.TotalAmount,
                Warranty = product.Warranty
            };
            return View(model);
        }
        public async Task<ActionResult> Cart()
        {
            ViewBag.IsCheckout = false;
            var prodList = await GetProductList();
            return View(prodList);
        }
        public async Task<ActionResult> Checkout()
        {
            var prodList = await GetProductList();
            ViewBag.IsCheckout = true;
            return View(prodList);
        }

        public string AddProductToCart(int productId)
        {
            string returnMsg = string.Empty;
            var products = System.Web.HttpContext.Current.Session["session_AddToCartProducts"] != null ? (List<int>)System.Web.HttpContext.Current.Session["session_AddToCartProducts"] : new List<int>();
            if (products.Contains(productId))
            {
                returnMsg = "Selected Product is already in Cart !";
            }
            else
            {
                products.Add(productId);
                returnMsg = "Product Added to Cart !";
                System.Web.HttpContext.Current.Session["session_AddToCartProducts"] = products;
            }
            return returnMsg;
        }
        public string RemoveProductFromCart(int productId)
        {
            string returnMsg = string.Empty;
            var products = System.Web.HttpContext.Current.Session["session_AddToCartProducts"] != null ? (List<int>)System.Web.HttpContext.Current.Session["session_AddToCartProducts"] : new List<int>();
            if (products.Contains(productId))
            {
                products.Remove(productId);
                System.Web.HttpContext.Current.Session["session_AddToCartProducts"] = products;
                returnMsg = "Product removed from Cart !";
            }
            return returnMsg;
        }
        public async Task<List<ProductViewModel>> GetProductList()
        {
            var products = System.Web.HttpContext.Current.Session["session_AddToCartProducts"] != null ? (List<int>)System.Web.HttpContext.Current.Session["session_AddToCartProducts"] : new List<int>();
            var model = await _productService.GetAllProducts();
            var productList = model.Where(x => products.Contains(x.ProductId)).ToList();
            var prodList = productList.Select(x => new ProductViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Color = x.Color,
                Description = x.Description,
                IsActive = x.IsActive,
                LaunchYear = x.LaunchYear,
                ModelName = x.ModelName,
                Price = x.Price,
                ProductDisplayImage = x.ProductDisplayImage,
                ProductId = x.ProductId,
                ProductImages = x.ProductImages,
                ProductName = x.ProductName,
                Qty = 1,
                TotalAmount = x.Price,
                Warranty = x.Warranty
            }).ToList();
            if (System.Web.HttpContext.Current.Session["session_UpdatedProducts"] != null)
            {
                var latestProdList = (List<ProductViewModel>)System.Web.HttpContext.Current.Session["session_UpdatedProducts"];
                prodList.ForEach(x =>
                {
                    if (latestProdList.Any(y => y.ProductId.Equals(x.ProductId)))
                    {
                        x.Qty = latestProdList.Where(y => y.ProductId.Equals(x.ProductId)).FirstOrDefault().Qty;
                        x.TotalAmount = latestProdList.Where(y => y.ProductId.Equals(x.ProductId)).FirstOrDefault().Qty * x.Price;
                    }
                    else { }
                });
            }
            return prodList;
        }
        [HttpPost]
        public void UpdateProductList(List<ProductViewModel> prodList)
        {
            System.Web.HttpContext.Current.Session["session_UpdatedProducts"] = prodList;
        }

    }
}
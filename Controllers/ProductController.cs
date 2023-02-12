using FarmersMarket_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace FarmersMarket_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public Response GetAllProduct()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("productCon").ToString());
            Response response = new Response();
            Application app = new Application();
            response = app.GetAllProducts(con);
            return response;
        }

        [HttpPost]
        [Route("addProduct")]
        public Response AddProduct(Product product)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("productCon").ToString());
            Response response = new Response();
            Application app = new Application();
            response = app.addProduct(con, product);
            return response;
        }

        [HttpPut]
        [Route("updateProduct")]
        public Response UpdateProduct(Product product)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("productCon").ToString());
            Response response = new Response();
            Application app = new Application();
            response = app.updateProduct(con, product);
            return response;
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public Response DeleteProduct(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("productCon").ToString());
            Response response = new Response();
            Application app = new Application();
            response = app.DeleteProduct(con, id);
            return response;
        }

        [HttpGet]
        [Route("getProductById/{id}")]
        public Response GetProduct(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("productCon").ToString());
            Response response = new Response();
            Application app = new Application();
            response = app.getProductById(con, id);
            return response;
        }
    }
}

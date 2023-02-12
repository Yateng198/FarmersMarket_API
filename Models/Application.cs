using System.Data;
using System.Data.SqlClient;

namespace FarmersMarket_API.Models
{
    public class Application
    {
        public Response GetAllProducts(SqlConnection con)
        {
            Response response = new Response(); 

            string query = "Select * from ProductTable";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Product> listPros = new List<Product>();
            if(dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    Product product = new Product();
                    product.productName = (string)dt.Rows[i]["ProductName"];
                    product.productId = (int)dt.Rows[i]["ProductID"];
                    product.amount = (int)dt.Rows[i]["Amount"];
                    product.price = float.Parse(dt.Rows[i]["Price"].ToString());
                    listPros.Add(product);
                }
            }
            if(listPros.Count > 0)
            {
                response.StatuesCode = 200;
                response.StatusMessage = "Products Information Retrieved Perfectly!";
                response.ListProducts = listPros;
            }
            else
            {
                response.StatuesCode = 100;
                response.StatusMessage = "No Product Found!";
                response.ListProducts = null;
            }
            return response;
        }

        public Response addProduct(SqlConnection con, Product product)
        {
            Response response = new Response();
            string query = "Insert into ProductTable (ProductId, ProductName, Amount, Price) Values( @id, @name, @amount, @price)";
            SqlCommand cmd =new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", product.productId);
            cmd.Parameters.AddWithValue("@name", product.productName);
            cmd.Parameters.AddWithValue("@amount", product.amount);
            cmd.Parameters.AddWithValue("@price", product.price);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i > 0)
            {
                response.StatuesCode = 200;
                response.StatusMessage = "Product Inserted Successfully!";
            }
            else
            {
                response.StatuesCode = 100;
                response.StatusMessage = "No data Inserted!";
            }
            return response;
        }

        public Response updateProduct(SqlConnection con, Product product)
        {
            Response response = new Response();
            string query = "Update ProductTable set ProductName = @name, Amount = @amount, Price = @price where ProductId = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", product.productName);
            cmd.Parameters.AddWithValue("@id", product.productId);
            cmd.Parameters.AddWithValue("@amount", product.amount);
            cmd.Parameters.AddWithValue("@price", product.price);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i > 0)
            {
                response.StatuesCode = 200;
                response.StatusMessage = "Product Information Updated!";
            }
            else
            {
                response.StatuesCode = 100;
                response.StatusMessage = "Product Information is Failed Updated!";
            }
            return response;
        }

        public Response DeleteProduct(SqlConnection con, int productId) 
        {
            Response response = new Response();
            string query = "Delete from ProductTable where ProductId = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", productId);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i > 0)
            {
                response.StatuesCode = 200;
                response.StatusMessage = "Product Found and Deleted!";
            }
            else
            {
                response.StatuesCode = 100;
                response.StatusMessage = "Product NOT Found and NOT Deleted!";
            }
            return response;
        }
        public Response getProductById(SqlConnection con, int productId)
        {
            Response response = new Response();
            string query = "Select * from ProductTable where ProductId = @id";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@id", productId);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                Product product = new Product();
                product.productId = (int)dt.Rows[0]["ProductId"];
                product.productName = (string)dt.Rows[0]["ProductName"];
                product.amount = (int)dt.Rows[0]["Amount"];
                product.price = float.Parse(dt.Rows[0]["Price"].ToString());

                response.StatuesCode = 200;
                response.StatusMessage = "Product Found!";
                response.Product = product;
            }
            else
            {
                response.StatuesCode = 100;
                response.StatusMessage = "Product Not Found!";
                response.Product = null;
            }
            return response;
        }
    }
}

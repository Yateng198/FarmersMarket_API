namespace FarmersMarket_API.Models
{
    public class Response
    {
        public int StatuesCode { get; set; }
        public string StatusMessage { get; set; }
        public Product Product { get; set; }
        public List<Product> ListProducts { get; set;}
    }
}

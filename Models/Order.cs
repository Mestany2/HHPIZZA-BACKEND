namespace HHPIZZA_BACKEND.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public string Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string OrderType { get; set; }
        public int? PaymentId { get; set; }
        public Payment payment { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public int? ReviewId { get; set; }
        public Review Review { get; set; }
        public decimal? Tip { get; set; }
        public List<Item> items { get; set; }
    }
}

namespace HHPIZZA_BACKEND.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public Order Order { get; set; }


    }
}

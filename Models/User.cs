namespace HHPIZZA_BACKEND.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Uid { get; set; }
        public bool isStaff { get; set; }
        public List<Order> Orders { get; set; }
    }
}

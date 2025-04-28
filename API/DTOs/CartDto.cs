namespace API.DTOs
{
    public class CartDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
    public class RemoveCartItemDto
    {
        public int ItemId { get; set; }
    }
}

namespace Restorator.Domain.Models
{
    public class ChangeRestaurantApprovalDTO
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public bool Approval { get; set; }
    }
}

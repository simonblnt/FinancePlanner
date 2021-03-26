namespace FinancePlanner.Models.UserSpecific
{
    public class ContactEmail
    {
        private int Id { get; set; }
        private int UserId { get; set; }
        private int ContactId { get; set; }
        private string Address { get; set; }
    }
}
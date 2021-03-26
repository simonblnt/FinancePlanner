namespace FinancePlanner.Models.UserSpecific
{
    public class ContactPhone
    {
        private int Id { get; set; }
        private int UserId { get; set; }
        private int ContactId { get; set; }
        private string Number { get; set; }
    }
}
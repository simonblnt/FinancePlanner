namespace FinancePlanner.Models.UserSpecific
{
    public class Contact
    {
        private int Id { get; set; }
        private int UserId { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private int CountryId { get; set; }
        private string Company { get; set; }
        private string Position { get; set; }
    }
}
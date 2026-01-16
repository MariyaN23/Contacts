namespace Contacts.DataAccess.Entities
{
    public class ContactEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string MobilePhone { get; set; }
        public required string JobTitle { get; set; }
        public required DateOnly BirthDate { get; set; }
    }
}

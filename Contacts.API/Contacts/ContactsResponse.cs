namespace Contacts.API.Contracts
{
    public record ContactsResponse(
        Guid id, 
        string name, 
        string mobilePhone, 
        string jobTitle,
        DateOnly birthDate);
}

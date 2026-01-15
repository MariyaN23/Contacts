namespace Contacts.API.Contracts
{
    public record ContactsRequest(
        string name,
        string mobilePhone,
        string jobTitle,
        DateOnly birthDate);
}

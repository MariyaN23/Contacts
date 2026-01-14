using Contacts.Core.Models;

namespace Contacts.Core.Abstractions
{
    public interface IContactsService
    {
        Task<List<Contact>> GetAllContacts();
        Task<Guid> CreateContact(Contact contact);
        Task<Guid> UpdateContact(Guid id, string name, string mobilePhone, string jobTitle, DateOnly birthDate);
        Task<Guid> DeleteContact(Guid id);
    }
}

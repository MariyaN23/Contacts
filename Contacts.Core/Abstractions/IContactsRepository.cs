using Contacts.Core.Models;

namespace Contacts.Core.Abstractions
{
    public interface IContactsRepository
    {
        Task<List<Contact>> Get();
        Task<Guid> Create(Contact contact);
        Task<Guid> Update(Guid id, string name, string mobilePhone, string jobTitle, DateOnly birthDate);
        Task<Guid> Delete(Guid id);
    }
}

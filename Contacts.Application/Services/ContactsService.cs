using Contacts.Core.Abstractions;
using Contacts.Core.Models;

namespace Contacts.Application.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        public ContactsService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            return await _contactsRepository.Get();
        }

        public async Task<Guid> CreateContact(Contact contact)
        {
            return await _contactsRepository.Create(contact);
        }

        public async Task<Guid> UpdateContact(Guid id, string name, string mobilePhone, string jobTitle, DateOnly birthDate)
        {
            return await _contactsRepository.Update(id, name, mobilePhone, jobTitle, birthDate);
        }

        public async Task<Guid> DeleteContact(Guid id)
        {
            return await _contactsRepository.Delete(id);
        }
    }
}

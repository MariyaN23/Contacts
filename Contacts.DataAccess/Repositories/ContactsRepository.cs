using Contacts.Core.Abstractions;
using Contacts.Core.Models;
using Contacts.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contacts.DataAccess.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly ContactsDbContext _context;
        public ContactsRepository(ContactsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> Get()
        {
            var contactEntities = await _context.Contacts
                .AsNoTracking()
                .ToListAsync();
            var contacts = contactEntities
                .Select(c => Contact.Create(c.Id, c.Name, c.MobilePhone, c.JobTitle, c.BirthDate).contact)
                .ToList();

            return contacts;
        }

        public async Task<Guid> Create(Contact contact)
        {
            var contactEntity = new ContactEntity
            {
                Id = contact.Id,
                MobilePhone = contact.MobilePhone,
                JobTitle = contact.JobTitle,
                BirthDate = contact.BirthDate,
            };
            await _context.Contacts.AddAsync(contactEntity);
            await _context.SaveChangesAsync();

            return contactEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string name, string mobilePhone, string jobTitle, DateOnly birthDate)
        {
            await _context.Contacts
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.Name, c => name)
                    .SetProperty(c => c.MobilePhone, c => mobilePhone)
                    .SetProperty(c => c.JobTitle, c => jobTitle)
                    .SetProperty(c => c.BirthDate, c => birthDate));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Contacts
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}

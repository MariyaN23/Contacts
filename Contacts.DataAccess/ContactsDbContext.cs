using Contacts.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contacts.DataAccess
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {
        }
        public DbSet<ContactEntity> Contact { get; set; }
    }
}

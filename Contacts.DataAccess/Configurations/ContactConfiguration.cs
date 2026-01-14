using Contacts.Core.Models;
using Contacts.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contacts.DataAccess.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<ContactEntity>
    {
        public void Configure(EntityTypeBuilder<ContactEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(Contact.MAX_NAME_LENGHT)
                .IsRequired();

            builder.Property(c => c.MobilePhone)
                .IsRequired();

            builder.Property(c => c.JobTitle)
                .HasMaxLength(Contact.MAX_JOB_TITLE_LENGHT)
                .IsRequired();

            builder.Property(c => c.BirthDate)
                .IsRequired();
        }
    }
}

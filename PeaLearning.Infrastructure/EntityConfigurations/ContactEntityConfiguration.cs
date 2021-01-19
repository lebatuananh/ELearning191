using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeaLearning.Domain.AggregateModels.Contact;

namespace PeaLearning.Infrastructure.EntityConfigurations
{
    public class ContactEntityConfiguration:IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("contacts");
        }
    }
}
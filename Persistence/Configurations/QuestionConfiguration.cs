using Atos.EFCore.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ConfigurationBase<Guid, Guid, Question>("Questions");
            builder.Property(q => q.Id).HasDefaultValue("NEWID()");
            builder.Property(q => q.State).HasColumnType("bit");
            builder.Property(q => q.Description).HasColumnType("varchar(120)");
            builder.Property(q => q.Order).HasColumnType("int");

            // I need the configuration for List<string> tags
            builder.Property(q => q.Tags).HasColumnType("varchar(120)");

        }
    }
}

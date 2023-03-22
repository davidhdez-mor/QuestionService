using Atos.EFCore.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class QuestionResponseConfiguration : IEntityTypeConfiguration<QuestionResponse>
    {
        public void Configure(EntityTypeBuilder<QuestionResponse> builder)
        {
            builder.ConfigurationBase<Guid, Guid, QuestionResponse>("QuestionResponses");
            builder.Property(q => q.Id).HasDefaultValue("NEWID()");
            builder.Property(q => q.State).HasColumnType("bit"); 
            builder.Property(x => x.ResourceId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.Property(x => x.ResourceName).HasColumnType("varchar(120)").IsRequired();
            builder.Property(x => x.ResQuestionResponse).HasColumnType("varchar(120)").IsRequired();
        }
    }
}

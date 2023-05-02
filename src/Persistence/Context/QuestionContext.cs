using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Atos.Core.Commons;

namespace Persistence.Context
{
    public class QuestionContext : DbContext
    {
        private readonly IDateTimeService _dateTime;

        public QuestionContext(DbContextOptions options, IDateTimeService dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        public DbSet<Question> Questions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new CancellationToken())
        {
            foreach (var entry in base.ChangeTracker.Entries<EntityBaseAuditable<Guid, Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Id = Guid.NewGuid();
                        entry.Entity.CreatedDate = _dateTime.NowUtc;
                        entry.Entity.State = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.DateLastModify = _dateTime.NowUtc;
                        break;
                    //break;               
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellation);
        }
    }
}
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

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
            //TODO: agregar despues la AuditableEntityBase
            foreach (var entry in base.ChangeTracker.Entries<EntityBaseAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTime.NowUtc;
                        entry.Entity.State = true;
                        break;
                        //case EntityState.Modified:                
                        //break;               
                        default:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellation);
        }
    }
}

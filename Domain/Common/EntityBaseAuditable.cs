using Atos.Core.Abstractions;

namespace Domain.Common
{
    public class EntityBaseAuditable : IEntityBase<Guid, Guid>, IEntityBaseAuditable<Guid, Guid>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public Guid UserCreatorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}

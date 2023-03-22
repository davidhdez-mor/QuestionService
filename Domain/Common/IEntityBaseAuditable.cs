namespace Domain.Common
{
    public interface IEntityBaseAuditable <TKey, TUserKey>
    {
        public DateTime? ModifiedDate { get; set; }
        public TUserKey ModifiedBy { get; set; }
    }
}

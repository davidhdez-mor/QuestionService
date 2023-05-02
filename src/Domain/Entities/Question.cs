using Atos.Core.Commons;

namespace Domain.Entities
{
    public class Question : EntityBaseAuditable<Guid,Guid>
    {
        public string Description { get; set; }
        public byte Order { get; set; }
        public string Tags { get; set; }   //Prescreening, BentchServices
    }
}
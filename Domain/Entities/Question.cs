using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Question : EntityBaseAuditable
    {
        public string Description { get; set; }
        public byte Order { get; set; }
        
        public IEnumerable<Tag> Tags { get; set; }   //Prescreening, BentchServices
    }

    public class Tag  //TODO QuestionTags
    {
        public Guid Id { get; set; }
        public string  TagName { get; set; }
    }
}

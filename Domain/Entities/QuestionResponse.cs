using Domain.Common;

namespace Domain.Entities
{
    public class QuestionResponse : EntityBaseAuditable
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string ResQuestionResponse { get; set; }
    }
}

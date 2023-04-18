    using Domain.Entities;

namespace Application.DTOs
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public byte Order { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public bool State { get; set; }
    }
}

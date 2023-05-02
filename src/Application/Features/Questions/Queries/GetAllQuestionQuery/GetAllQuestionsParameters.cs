using Application.Parameters;

namespace Application.Features.Questions.Queries.GetAllQuestionQuery;

public class GetAllQuestionsParameters : RequestParameter
{
    public string? Description { get; set; }
    public byte? Order { get; set; }
    public string? Tags { get; set; }
}
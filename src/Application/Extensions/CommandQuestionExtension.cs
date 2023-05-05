using Application.Features.Questions.Commands.DeleteQuestionCommand;
using Application.Features.Questions.Commands.UpdateQuestionCommand;
using Atos.Core.EventsDTO;

namespace Application.Extensions;

public static class CommandQuestionExtension
{
    public static QuestionUpdated ToQuestionUpdated(this UpdateQuestionCommand request)
    {
        return new QuestionUpdated()
        {
            Description = request.Description,
            Id = request.Id,
            Order = request.Order,
            Tags = request.Tags
        };
    }
    
    public static QuestionDeleted ToQuestionDeleted(this DeleteQuestionCommand request)
    {
        return new QuestionDeleted()
        {
            Id = request.Id
        };
    }
}
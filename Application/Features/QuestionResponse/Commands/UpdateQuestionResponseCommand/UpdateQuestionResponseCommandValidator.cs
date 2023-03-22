using FluentValidation;

namespace Application.Features.QuestionResponse.Commands.UpdateQuestionResponseCommand
{
    public class UpdateQuestionResponseCommandValidator : AbstractValidator<UpdateQuestionResponseCommand>
    {
        public UpdateQuestionResponseCommandValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(q => q.ResourceId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(q => q.ResourceName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(120).WithMessage("{PropertyName} max length is {MaxLength}");
            RuleFor(q => q.ResQuestionResponse)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(200).WithMessage("{PropertyName} max length is {MaxLength}"); ;
        }
    }
}

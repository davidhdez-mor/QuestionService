using FluentValidation;

namespace Application.Features.QuestionResponse.Commands.CreateQuestionResponseCommand
{
    public class CreateQuestionResponseCommandValidator : AbstractValidator<CreateQuestionResponseCommand>
    {
        public CreateQuestionResponseCommandValidator()
        {
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

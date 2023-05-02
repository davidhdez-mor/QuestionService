using FluentValidation;

namespace Application.Features.Questions.Commands.UpdateQuestionCommand
{
    public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(q => q.Description)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .MaximumLength(200).WithMessage("{PropertyName} max length is {MaxLength}");

            RuleFor(q => q.Order)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            
            RuleFor(q => q.Tags)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(20).WithMessage("{PropertyName} max length is {MaxLength}");
        }
    }
}

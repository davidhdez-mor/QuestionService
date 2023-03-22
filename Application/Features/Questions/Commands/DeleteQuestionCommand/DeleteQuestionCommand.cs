using Application.Exceptios;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Questions.Commands.DeleteQuestionCommand
{
    public class DeleteQuestionCommand : IRequest<Response<Question>>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
    }

    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Response<Question>>
    {
        private readonly IRepositoryAsync<Question> _repositoryAsync;

        public DeleteQuestionCommandHandler(IRepositoryAsync<Question> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Question>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _repositoryAsync.GetByIdAsync(request.Id);
            if (question == null)
            {
                throw new ApiExceptions($"{request.Id} not found");
            }
            else
            {
                question.State = request.State;
                await _repositoryAsync.UpdateAsync(question);
                return new Response<Question>(question);
            }
        }
    }
}

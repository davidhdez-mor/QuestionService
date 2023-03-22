using Application.Exceptios;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.QuestionResponse.Commands.DeleteQuestionResponseCommand
{
    public class DeleteQuestionResponseCommand :IRequest<Response<Domain.Entities.QuestionResponse>>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
    }

    public class DeleteQuestionResponseCommandHandler : IRequestHandler<DeleteQuestionResponseCommand, Response<Domain.Entities.QuestionResponse>>
    {
        private readonly IRepositoryAsync<Domain.Entities.QuestionResponse> _repositoryAsync;

        public DeleteQuestionResponseCommandHandler(IRepositoryAsync<Domain.Entities.QuestionResponse> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Domain.Entities.QuestionResponse>> Handle(DeleteQuestionResponseCommand request, CancellationToken cancellationToken)
        {
            var questionResponse = await _repositoryAsync.GetByIdAsync(request.Id);
            if (questionResponse == null)
            {
                throw new ApiExceptions($"{request.Id} not found");
            }
            else
            {
                questionResponse.State = request.State;
                await _repositoryAsync.UpdateAsync(questionResponse);
                return new Response<Domain.Entities.QuestionResponse>(questionResponse);
            }
        }
    }
}

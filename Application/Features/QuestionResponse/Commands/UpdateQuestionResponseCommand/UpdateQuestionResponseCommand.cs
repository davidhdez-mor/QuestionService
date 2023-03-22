using Application.Exceptios;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.QuestionResponse.Commands.UpdateQuestionResponseCommand
{
    public class UpdateQuestionResponseCommand : IRequest<Response<Domain.Entities.QuestionResponse>>
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string ResQuestionResponse { get; set; }
    }

    public class UpdateQuestionResponseCommandHandler : IRequestHandler<UpdateQuestionResponseCommand, Response<Domain.Entities.QuestionResponse>>
    {
        private readonly IRepositoryAsync<Domain.Entities.QuestionResponse> _repositoryAsync;

        public UpdateQuestionResponseCommandHandler(IRepositoryAsync<Domain.Entities.QuestionResponse> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Domain.Entities.QuestionResponse>> Handle(UpdateQuestionResponseCommand request, CancellationToken cancellationToken)
        {
            var questionResponse = await _repositoryAsync.GetByIdAsync(request.Id);
            if (questionResponse == null)
            {
                throw new ApiExceptions($"{request.Id} not found");
            }
            else
            {
                questionResponse.ResourceId = request.ResourceId;
                questionResponse.ResourceName = request.ResourceName;
                questionResponse.ResQuestionResponse = request.ResQuestionResponse;
                await _repositoryAsync.UpdateAsync(questionResponse);
                return new Response<Domain.Entities.QuestionResponse>(questionResponse);
            }
        }
    }
}

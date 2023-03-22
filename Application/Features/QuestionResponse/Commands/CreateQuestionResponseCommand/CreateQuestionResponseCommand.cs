using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.QuestionResponse.Commands.CreateQuestionResponseCommand
{
    public class CreateQuestionResponseCommand : IRequest<Response<Domain.Entities.QuestionResponse>>
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string ResQuestionResponse { get; set; }
    }

    public class CreateQuestionResponseCommandHandler : IRequestHandler<CreateQuestionResponseCommand, Response<Domain.Entities.QuestionResponse>>
    {
        private readonly IRepositoryAsync<Domain.Entities.QuestionResponse> _repositoryAsync;
        private readonly IMapper _mapper;
        public CreateQuestionResponseCommandHandler(IRepositoryAsync<Domain.Entities.QuestionResponse> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<Domain.Entities.QuestionResponse>> Handle(CreateQuestionResponseCommand request, CancellationToken cancellationToken)
        {
            var questionResponse = _mapper.Map<Domain.Entities.QuestionResponse>(request);
            await _repositoryAsync.AddAsync(questionResponse);
            return new Response<Domain.Entities.QuestionResponse>(questionResponse);
        }
    }
}

using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.QuestionResponse.Queries.GetAllQuestionResponseQuery
{
    public class GetAllQuestionResponseQuery : IRequest<Response<List<QuestionResponseDTO>>>
    {

    }

    public class GetAllQuestionResponseQueryHandler : IRequestHandler<GetAllQuestionResponseQuery, Response<List<QuestionResponseDTO>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.QuestionResponse> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllQuestionResponseQueryHandler(IRepositoryAsync<Domain.Entities.QuestionResponse> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<QuestionResponseDTO>>> Handle(GetAllQuestionResponseQuery request, CancellationToken cancellationToken)
        {
            var questionResponses = await _repositoryAsync.ListAsync();
            var questionsResponsesDTO = _mapper.Map<List<QuestionResponseDTO>>(questionResponses);
            return new Response<List<QuestionResponseDTO>>(questionsResponsesDTO);
        }
    }
}

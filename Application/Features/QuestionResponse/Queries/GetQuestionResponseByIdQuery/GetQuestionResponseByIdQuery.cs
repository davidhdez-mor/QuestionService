using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.QuestionResponse.Queries.GetQuestionResponseByIdQuery
{
    public class GetQuestionResponseByIdQuery : IRequest<Response<QuestionResponseDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetQuestionResponseByIdQueryHandler : IRequestHandler<GetQuestionResponseByIdQuery, Response<QuestionResponseDTO>>
    {
        private readonly IRepositoryAsync<Domain.Entities.QuestionResponse> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetQuestionResponseByIdQueryHandler(IRepositoryAsync<Domain.Entities.QuestionResponse> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<QuestionResponseDTO>> Handle(GetQuestionResponseByIdQuery request, CancellationToken cancellationToken)
        {
            var questionResponse = await _repositoryAsync.GetByIdAsync(request.Id);
            var questionResponseDTO = _mapper.Map<QuestionResponseDTO>(questionResponse);
            return new Response<QuestionResponseDTO>(questionResponseDTO);
        }
    }
}

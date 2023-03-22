using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Questions.Queries.GetAllQuestionQuery
{
    public class GetAllQuestionQuery : IRequest<Response<List<QuestionDTO>>>
    {
    }

    public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, Response<List<QuestionDTO>>>
    {
        private readonly IRepositoryAsync<Question> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllQuestionQueryHandler(IRepositoryAsync<Question> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<QuestionDTO>>> Handle(GetAllQuestionQuery request, CancellationToken cancellationToken)
        {
            var questions = await _repositoryAsync.ListAsync();
            var questionsDTO = _mapper.Map<List<QuestionDTO>>(questions);
            return new Response<List<QuestionDTO>>(questionsDTO);
        }
    }
}

using Application.DTOs;
using Application.Interfaces;
using Application.Specification;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Questions.Queries.GetAllQuestionQuery
{
    public class GetAllQuestionQuery : IRequest<Response<List<QuestionDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Description { get; set; }
        public byte? Order { get; set; }
        public string Tags { get; set; }
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
            var pagination = new PagedQuestionSpecification(request.PageSize, request.PageNumber, request.Description,
                request.Order, request.Tags);
            var questions = await _repositoryAsync.ListAsync(pagination);
            var questionsDTO = _mapper.Map<List<QuestionDTO>>(questions);
            return new Response<List<QuestionDTO>>(questionsDTO);
        }
    }
}

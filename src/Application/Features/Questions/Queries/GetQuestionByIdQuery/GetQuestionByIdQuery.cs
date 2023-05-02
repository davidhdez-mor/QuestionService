using Application.DTOs;
using Application.Exceptios;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Runtime.ExceptionServices;

namespace Application.Features.Questions.Queries.GetQuestionByIdQuery
{
    public class GetQuestionByIdQuery : IRequest<Response<QuestionDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, Response<QuestionDTO>>
    {
        private readonly IRepositoryAsync<Question> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetQuestionByIdQueryHandler(IRepositoryAsync<Question> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<Response<QuestionDTO>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ApiExceptions();
            }

            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<QuestionDTO>> HandleProcess(GetQuestionByIdQuery request,
            CancellationToken cancellation)
        {
            var question = await _repositoryAsync.GetByIdAsync(request.Id);
            var questionDTO = _mapper.Map<QuestionDTO>(question);
            if (question == null)
                throw new ApiExceptions($"{question.Id} not found");
            
            return new Response<QuestionDTO>(questionDTO);
        }
    }
}
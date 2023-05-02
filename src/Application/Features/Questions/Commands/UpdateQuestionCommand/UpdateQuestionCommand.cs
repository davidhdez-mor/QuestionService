using Application.Exceptios;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Questions.Commands.UpdateQuestionCommand
{
    public class UpdateQuestionCommand : IRequest<Response<Question>>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public byte Order { get; set; }
        public string Tags { get; set; }

    }

    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Response<Question>>
    {
        private readonly IRepositoryAsync<Question> _repositoryAsync;
        public UpdateQuestionCommandHandler(IRepositoryAsync<Question> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
        }

        public Task<Response<Question>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ApiExceptions("Question not found");

            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<Question>> HandleProcess(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _repositoryAsync.GetByIdAsync(request.Id);

            if (question == null)
                throw new ApiExceptions($"{request.Id} not found");

            question.Description = request.Description;
            question.Order = request.Order;
            question.Tags = request.Tags;
            
            await _repositoryAsync.UpdateAsync(question);
            return new Response<Question>(question);
        }


    }
}
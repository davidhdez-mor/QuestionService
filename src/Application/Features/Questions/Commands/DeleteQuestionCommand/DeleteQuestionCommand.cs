using Application.Exceptios;
using Application.Extensions;
using Application.Interfaces;
using Application.Wrappers;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MediatR;

namespace Application.Features.Questions.Commands.DeleteQuestionCommand
{
    public class DeleteQuestionCommand : IRequest<Response<Question>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Response<Question>>
    {
        private readonly IRepositoryAsync<Question> _repositoryAsync;
        private readonly IPublisherCommands<QuestionDeleted> _publisherCommands;

        public DeleteQuestionCommandHandler(IRepositoryAsync<Question> repositoryAsync,
            IPublisherCommands<QuestionDeleted> publisherCommands)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
        }

        public async Task<Response<Question>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _repositoryAsync.GetByIdAsync(request.Id);
            if (question == null)
                throw new ApiExceptions($"{request.Id} not found");

            if ((question.Description != null))
                throw new ApiExceptions("No changes can be made, the question has already been answered");

            question.State = false;
            await _repositoryAsync.UpdateAsync(question);
            await _publisherCommands.PublishEntityMessage(request.ToQuestionDeleted(), "question.deleted", request.Id,
                cancellationToken);

            return new Response<Question>(question);
        }
    }
}
using Application.Exceptios;
using Application.Extensions;
using Application.Interfaces;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using AutoMapper;
using Domain.Entities;
using MassTransit;
using MediatR;

namespace Application.Features.Questions.Commands.UpdateQuestionCommand
{
    public class UpdateQuestionCommand : IRequest<Wrappers.Response<Question>>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public byte Order { get; set; }
        public string Tags { get; set; }

    }

    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Wrappers.Response<Question>>
    {
        private readonly IRepositoryAsync<Question> _repositoryAsync;
        private readonly IPublisherCommands<QuestionUpdated> _publisherCommands;
        public UpdateQuestionCommandHandler(IRepositoryAsync<Question> repositoryAsync, IPublisherCommands<QuestionUpdated> publisherCommands)
        {
            _repositoryAsync = repositoryAsync;
            _publisherCommands = publisherCommands;
        }

        public Task<Wrappers.Response<Question>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ApiExceptions("Question not found");

            return HandleProcess(request, cancellationToken);
        }

        public async Task<Wrappers.Response<Question>> HandleProcess(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _repositoryAsync.GetByIdAsync(request.Id);

            if (question == null)
                throw new ApiExceptions($"{request.Id} not found");

            question.Description = request.Description;
            question.Order = request.Order;
            question.Tags = request.Tags;
            
            await _repositoryAsync.UpdateAsync(question);
            await _publisherCommands.PublishEntityMessage(request.ToQuestionUpdated(), "question.updated", request.Id, cancellationToken);
            return new Wrappers.Response<Question>(question);
        }
    }
}
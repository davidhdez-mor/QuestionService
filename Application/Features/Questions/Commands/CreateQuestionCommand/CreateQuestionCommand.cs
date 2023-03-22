using Application.Wrappers;
using MediatR;
using Domain.Entities;
using Application.Interfaces;
using AutoMapper;

namespace Application.Features.Questions.Commands.CreateQuestionCommand
{
    public class CreateQuestionCommand : IRequest<Response<Question>>
    {
        public string Description { get; set; }
        public byte Order { get; set; }
    }

    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Response<Question>>
    {
        private readonly IRepositoryAsync<Question> _repositoryAsync;
        private readonly IMapper _mapper;
        public CreateQuestionCommandHandler(IRepositoryAsync<Question> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<Response<Question>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Exception();
            }
            return HandleProcess(request, cancellationToken);
        }

        public async Task<Response<Question>> HandleProcess(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = _mapper.Map<Question>(request);
            var data = await _repositoryAsync.AddAsync(question);

            return new Response<Question>(data);
        }
    }

}

using Application.Features.QuestionResponse.Commands.CreateQuestionResponseCommand;
using Application.Features.QuestionResponse.Commands.DeleteQuestionResponseCommand;
using Application.Features.QuestionResponse.Commands.UpdateQuestionResponseCommand;
using Application.Features.QuestionResponse.Queries.GetAllQuestionResponseQuery;
using Application.Features.QuestionResponse.Queries.GetQuestionResponseByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace QuestionServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionResponseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public QuestionResponseController(IMediator medator)
        {
            _mediator = medator;
        }

        [HttpPost("/CreateQuestionResponse")]
        public async Task<IActionResult> CreateQuestionResponse(CreateQuestionResponseCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("/DeleteQuestionResponse")]
        public async Task<IActionResult> DeleteQuestionResponse(DeleteQuestionResponseCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("/UpdateQuestionResponse")]
        public async Task<IActionResult> UpdateQuestionResponse(UpdateQuestionResponseCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/GetAllQuestionResponse")]
        public async Task<IActionResult> GetAllQuestionResponse()
        {
            var result = await _mediator.Send(new GetAllQuestionResponseQuery());
            return Ok(result);
        }

        [HttpGet("/GetQuestionResponseById/{id}")]
        public async Task<IActionResult> GetQuestionResponseById(Guid id)
        {
            var result = await _mediator.Send(new GetQuestionResponseByIdQuery() { Id = id });
            return Ok(result);
        }
    }
}

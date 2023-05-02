using Application.Features.Questions.Commands.CreateQuestionCommand;
using Application.Features.Questions.Commands.DeleteQuestionCommand;
using Application.Features.Questions.Commands.UpdateQuestionCommand;
using Application.Features.Questions.Queries.GetAllQuestionQuery;
using Application.Features.Questions.Queries.GetQuestionByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace QuestionServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public QuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/CreateQuestion")]
        public async Task<IActionResult> CreateQuestion( CreateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetQuestionById", new {id = result.Data.Id} , command );
        }

        [HttpPut("/UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion(UpdateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("/DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion(DeleteQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("/GetQuestionById/{id}", Name = "GetQuestionById")]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            var result = await _mediator.Send(new GetQuestionByIdQuery() { Id = id });
            return Ok(result);
        }

        [HttpGet("/GetAllQuestion")]
        public async Task<IActionResult> GetAllQuestion([FromQuery]GetAllQuestionsParameters filters)
        {
            return Ok(await _mediator.Send(new GetAllQuestionQuery 
            {
                PageNumber = filters.PageNumber, 
                PageSize = filters.PageSize,
                Description = filters.Description,
                Order = filters.Order,
                Tags = filters.Tags
            }));
        }
    }
}

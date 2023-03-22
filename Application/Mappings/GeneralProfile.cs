using Application.DTOs;
using Application.Features.QuestionResponse.Commands.CreateQuestionResponseCommand;
using Application.Features.Questions.Commands.CreateQuestionCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs
            CreateMap<Question, QuestionDTO>();
            CreateMap<QuestionResponse, QuestionResponseDTO>();
            #endregion

            #region Commands
            CreateMap<CreateQuestionCommand, Question>();
            CreateMap<CreateQuestionResponseCommand, QuestionResponse>();
            #endregion

        }
    }
}

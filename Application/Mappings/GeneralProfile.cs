using Application.DTOs;
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
   
            #endregion

            #region Commands
            CreateMap<CreateQuestionCommand, Question>();

            #endregion

        }
    }
}

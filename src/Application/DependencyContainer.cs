using Application.Behaviours;
using Application.Features.Questions.Commands.CreateQuestionCommand;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.Commons.Publishers;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MassTransit;

namespace Application
{
    public static class DependencyContainer
    {
        public static void AddAplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(typeof(CreateQuestionCommand).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped<IPublisherCommands<QuestionUpdated>, PublisherCommands<QuestionUpdated>>();
            services.AddScoped<IPublisherCommands<QuestionDeleted>, PublisherCommands<QuestionDeleted>>();

            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq((ctx, cfgrmq) =>
                {
                    cfgrmq.Host("amqp://guest:guest@localhost:5672");

                    cfgrmq.Publish<QuestionUpdated>(x => { x.ExchangeType = "topic"; });

                    cfgrmq.Publish<QuestionDeleted>(x => { x.ExchangeType = "topic"; });
                });
            });
        }
    }
}
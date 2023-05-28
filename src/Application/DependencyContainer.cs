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
using MassTransit;

namespace Application
{
	public static class DependencyContainer
	{
		public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
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
					cfgrmq.Host(GetMessageBrokerUrl());

					cfgrmq.Publish<QuestionUpdated>(x => { x.ExchangeType = "topic"; });

					cfgrmq.Publish<QuestionDeleted>(x => { x.ExchangeType = "topic"; });
				});
			});
		}

		private static string GetMessageBrokerUrl()
		{
			var user = Environment.GetEnvironmentVariable("MQUSER");
			var password = Environment.GetEnvironmentVariable("MQPASSWORD");
			var host = Environment.GetEnvironmentVariable("MQHOST");
			var port = Environment.GetEnvironmentVariable("MQPORT");
			var url = $"amqp://{user}:{password}@{host}:{port}";
			return url;
		}
	}
}

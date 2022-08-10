using EmailService.Interfaces;
using FluentEmail.Core;
using FotballersAPI.Domain.Enums;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace EmailService.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private const string _templatePath = "EmailService.Services.EmailLayouts.{0}.cshtml";
        private readonly IFluentEmail _fluentEmail;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(IFluentEmail fluentEmail, ILogger<EmailSenderService> logger)
        {
            _fluentEmail = fluentEmail;
            _logger = logger;
        }

        public async Task<bool> SendAsync(string to, 
            string subject, 
            EmailTemplate emailTemplate, 
            object model)
        {
            var result = await _fluentEmail.To(to)
                .Subject(subject)
                .UsingTemplateFromEmbedded(string.Format(_templatePath, emailTemplate),
                    ToExpando(model), 
                    GetType().Assembly)
                .SendAsync();

            if(!result.Successful)
            {
                _logger.LogError($"Failed to send an email '{result.ErrorMessages}'");
            }

            return result.Successful;
        }

        private static ExpandoObject ToExpando(object model)
        {
            if (model is ExpandoObject exp)
            {
                return exp;
            }

            IDictionary<string, object> expando = new ExpandoObject();

            foreach (var propertyDescriptor in model.GetType().GetTypeInfo().GetProperties())
            {
                var obj = propertyDescriptor.GetValue(model);

                if (obj is not null && IsAnonymousType(obj.GetType()))
                {
                    obj = ToExpando(obj);
                }

                expando.Add(propertyDescriptor.Name, obj);
            }

            return (ExpandoObject)expando;
        }

        private static bool IsAnonymousType(Type type)
        {
            bool hasCompilerGeneratedAttribute = type.GetTypeInfo()
                .GetCustomAttributes(typeof(CompilerGeneratedAttribute), false)
                .Any();

            bool nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            bool isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;

            return isAnonymousType;
        }
    }
}

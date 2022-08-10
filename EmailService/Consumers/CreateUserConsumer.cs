using EmailService.Interfaces;
using FotballersAPI.Domain.Enums;
using FotballersAPI.Domain.Events.Users;
using MassTransit;

namespace EmailService.Handlers
{
    public class CreateUserConsumer : IConsumer<UserCreated>
    {
        private readonly IEmailSenderService _emailSenderService;

        public CreateUserConsumer(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var data = context.Message;

            await _emailSenderService.SendAsync(data.Email, 
                $"User {data.Username} created, active account",
                EmailTemplate.ActiveAccountMessage,
                data);
        }
    }
}

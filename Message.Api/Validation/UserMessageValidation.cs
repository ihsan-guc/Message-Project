using FluentValidation;
using Message.Api.Models.Request;
using Message.Data.DAL;

namespace Message.Api.Validation
{
    public class UserMessageValidation : AbstractValidator<UserMessageRequest>
    {
        public UserMessageValidation(MessageContext messageContext)
        {
            RuleFor(p => p.SenderApplicationUserId).NotEmpty().WithMessage("Gönderici Id Boş Olamaz");
            RuleFor(p => p.ReceiverApplicationUserId).NotEmpty().WithMessage("Alici Id Boş Olamaz");
            RuleFor(p => p.MessageText).NotEmpty().WithMessage("Mesaj Boş Olamaz");
        }
    }
}

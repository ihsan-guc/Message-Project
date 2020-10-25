using FluentValidation;
using Message.Api.Models.Request;
using Message.Data.DAL;
using System.Linq;

namespace Message.Api.Validation
{
    public class RegisterValidation : AbstractValidator<RegisterRequest>
    {
        public RegisterValidation(MessageContext messageContext)
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("UserName Boş Olamaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password Boş Olamaz");
            RuleFor(p => p.Email).EmailAddress().NotEmpty().WithMessage("Email Boş Olamaz");
            RuleFor(p => p).Custom((model, context) =>
            {
                var email = messageContext.ApplicationUsers.FirstOrDefault(p => p.Email == model.Email);
                if (email != null)
                    context.AddFailure("Bu e-posta daha önce alınmış!");
            });
        }
    }
}

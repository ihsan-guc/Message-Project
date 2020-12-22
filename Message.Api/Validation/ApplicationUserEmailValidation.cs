using FluentValidation;
using Message.Api.Models.Request;
using Message.Data.DAL;
using Message.Data.Domain.Entities;
using System.Linq;

namespace Message.Api.Validation
{
    public class ApplicationUserEmailValidation :AbstractValidator<ChangePasswordRequest>
    {
        public ApplicationUserEmailValidation(MessageContext messageContext)
        {
            RuleFor(p => p.Email).EmailAddress().NotEmpty().WithMessage("Email Boş Olamaz.");
            RuleFor(p => p).Custom((model, context) =>
            {
                var email = messageContext.ApplicationUsers.FirstOrDefault(p => p.Email == model.Email);
                if (email == null)
                    context.AddFailure("Kayıtlı Epost Yoktur!!!");
            });
        }
    }
}

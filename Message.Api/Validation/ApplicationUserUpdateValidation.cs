using FluentValidation;
using Message.Api.Models.Request;
using Message.Data.DAL;
using System.Linq;

namespace Message.Api.Validation
{
    public class ApplicationUserUpdateValidation : AbstractValidator<ApplicaitionUserUpdateRequest>
    {
        public ApplicationUserUpdateValidation(MessageContext messageContext)
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("UserName Boş Olamaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password Boş Olamaz");
            
            //RuleFor(p => p.Email).EmailAddress().NotEmpty().WithMessage("Email Boş Olamaz");
            
            RuleFor(p => p).Custom((model, context) =>
            {
                //var email = messageContext.ApplicationUsers.FirstOrDefault(p => p.Email == model.Email);
                var user = messageContext.ApplicationUsers.FirstOrDefault(p => p.Id == model.Id);
                //if (email != null)
                //    context.AddFailure("Bu e-posta daha önce alınmış!");
                if (user == null)
                    context.AddFailure(" Kullanıcı yok");
            });
        }
    }
}

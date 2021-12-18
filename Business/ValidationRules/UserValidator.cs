using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {

            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();

            RuleFor(u => u.FirsName).NotEmpty();
            RuleFor(u => u.FirsName).MinimumLength(3);

            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.LastName).MinimumLength(2);

        }
    }
}

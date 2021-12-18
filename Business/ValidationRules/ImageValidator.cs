using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules
{
    public class ImageValidator : AbstractValidator<Image>
    {
        public ImageValidator()
        {
            RuleFor(i => i.CarId).NotEmpty();

            RuleFor(i => i.ImagePath).NotEmpty();
        }
    }
}

using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name).NotEmpty();
            RuleFor(b => b.Name).MinimumLength(3);

            //RuleFor(b => b.Name).Must(StartsWithA).WithMessage("Markalar A harfi ile başlamalı");
        }

        private bool StartsWithA(string arg)
        {
            return arg.StartsWith("A") || arg.StartsWith("a");
        }
    }
}

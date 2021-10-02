using Basket.Core.Constants;
using Basket.Models.Dtos;
using FluentValidation;

namespace Basket.Services.ValidationRules.FluentValidation
{
    public class BasketDtoValidator : AbstractValidator<BasketDto>
    {
        public BasketDtoValidator()
        {
            RuleFor(r => r.UserId).NotEmpty().WithMessage(BasketMessages.UserIdValidationMessage);
            RuleFor(r => r.BasketItems).NotEmpty().WithMessage(BasketMessages.BasketItemsValidationMessage);
            RuleForEach(r => r.BasketItems)
                .ChildRules(item =>
                {
                    item.RuleFor(x => x.Price).NotEmpty().WithMessage(BasketMessages.PriceValidationMessage);
                    item.RuleFor(x => x.ProductCode).NotEmpty().WithMessage(BasketMessages.ProductCodeValidationMessage);
                    item.RuleFor(x => x.Quantity).NotEmpty().WithMessage(BasketMessages.QuantityValidationMessage);
                })
                .NotEmpty();
        }
    }
}

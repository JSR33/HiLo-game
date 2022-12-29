using FluentValidation;
using HiLoGame.Contracts.v1.Requests;

namespace HiLoGame.Backend.Validators
{
    public class PlayerInfoRequestValidator : AbstractValidator<PlayerInfoRequest>
    {
        /// <summary>
        /// Player information request validator rules
        /// </summary>
        public PlayerInfoRequestValidator()
        {
            RuleFor(_=>_.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.Age)
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(99);
        }
    }
}

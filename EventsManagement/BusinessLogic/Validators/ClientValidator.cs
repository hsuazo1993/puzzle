using DTOs;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class ClientValidator : AbstractValidator<ClientDto>
    {
        public ClientValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(MessageConstants.MsgNameIsRequired)
                .MaximumLength(100).WithMessage(MessageConstants.MsgInvalidNameLength);

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(MessageConstants.MsgEmailIsRequired)
                .MaximumLength(100).WithMessage(MessageConstants.MsgInvalidEmailLength)
                .EmailAddress().WithMessage(MessageConstants.MsgInvalidEmailFormat);

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage(MessageConstants.MsgPhoneIsRequired)
                .MaximumLength(20).WithMessage(MessageConstants.MsgInvalidPhoneLength);

            RuleFor(c => c.StatusId)
                .NotEmpty().WithMessage(MessageConstants.MsgStatusIsRequired);

            RuleFor(c => c.DateOfBirth)
                .NotEmpty().WithMessage(MessageConstants.MsgDateOfBirthIsRequired)
                .Must(BeValidDateOfBirth).WithMessage(MessageConstants.MsgInvalidDateOfBirth);
        }

        private bool BeValidDateOfBirth(DateTime dateOfBirth)
        {
            return dateOfBirth < DateTime.Now;
        }
    }
}

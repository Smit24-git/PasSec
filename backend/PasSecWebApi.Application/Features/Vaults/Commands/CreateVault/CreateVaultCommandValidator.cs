using FluentValidation;

namespace PasSecWebApi.Application.Features.Vaults.Commands.CreateVault
{
    public class CreateVaultCommandValidator : AbstractValidator<CreateVaultCommand>
    {
        public CreateVaultCommandValidator()
        {
            RuleFor(e => e.VaultName)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e)
                .Must(HaveUserKeyIfChecked)
                .WithMessage("User Key must be 32 char long.");

            RuleForEach(e => e.Keys)
                .SetValidator(new CreateVaultCommandKeysValidator());
        }

        private bool HaveUserKeyIfChecked(CreateVaultCommand command)
        {
            if (command.UseUserKey)
            {
                return !string.IsNullOrWhiteSpace(command.UserKey) &&
                        command.UserKey.Length == 32;
            }
            return true;
        }
    }
    public class CreateVaultCommandKeysValidator : AbstractValidator<CreateVaultCommandKey>
    {
        public CreateVaultCommandKeysValidator()
        {
            RuleFor(e => e.Password)
                .NotNull()
                .NotEmpty();
            RuleForEach(e => e.SecurityQuestions)
                .SetValidator(new CreateVaultCommandSecurityQAValidator());
        }
    }

    public class CreateVaultCommandSecurityQAValidator : AbstractValidator<CreateVaultCommandKeySecurityQuestion>
    {
        public CreateVaultCommandSecurityQAValidator()
        {
            RuleFor(e => e.Question)
                .NotEmpty()
                .NotNull();
            RuleFor(e => e.Answer)
                .NotEmpty()
                .NotNull();
        }
    }
}

using FluentValidation;

namespace ITCompanyCVManager.Boundary.Validators;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> MustBeEmailAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches(@"^[\w-+\.]+@([\w-]+\.)+[\w-]{2,4}$");
    }

    public static IRuleBuilderOptions<T, string> MustBePhone<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches(@"^\(\d{3}\)\s?\d{3}-\d{4}$").WithMessage($"String must be Phone format '(000) 000-0000'.");
    }
}
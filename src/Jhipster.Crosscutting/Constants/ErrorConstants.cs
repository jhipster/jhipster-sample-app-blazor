namespace Jhipster.Crosscutting.Constants;

public static class ErrorConstants
{
    public const string ErrValidation = "error.validation";
    private const string ProblemBaseUrl = "https://www.jhipster.tech/problem";
    public static readonly string DefaultType = $"{ProblemBaseUrl}/problem-with-message";
    public static readonly string ConstraintViolationType = $"{ProblemBaseUrl}/constraint-violation";
    public static readonly string ParametrizedType = $"{ProblemBaseUrl}/parametrized";
    public static readonly string EntityNotFoundType = $"{ProblemBaseUrl}/entity-not-found";
    public static readonly string InvalidPasswordType = $"{ProblemBaseUrl}/invalid-password";
    public static readonly string EmailAlreadyUsedType = $"{ProblemBaseUrl}/email-already-used";
    public static readonly string LoginAlreadyUsedType = $"{ProblemBaseUrl}/login-already-used";
    public static readonly string EmailNotFoundType = $"{ProblemBaseUrl}/email-not-found";
}

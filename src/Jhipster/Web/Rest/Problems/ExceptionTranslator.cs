namespace Jhipster.Web.Rest.Problems;

public class ExceptionTranslator
{
    //        public const string FieldErrorsKey = "fieldErrors";
    //        public const string MessageKey = "message";
    //        public const string PathKey = "path";
    //        public const string ViolationsKey = "violations";
    //
    //
    //        public override ProblemResult Process(ProblemResult result)
    //        {
    //            if (result == null) return result;
    //
    //            var problem = (IProblem) result.Value;
    ////            if (!(problem is ConstraintViolationProblem || problem is DefaultProblem)) {
    //            if (!(problem is DefaultProblem)) return result;
    //
    //            var builder = ProblemBuilder.Builder()
    //                .WithType(ProblemConstants.DefaultType.Equals(problem.Type) ? ErrorConstants.DefaultType : problem.Type)
    //                .WithStatus(problem.Status)
    //                .WithTitle(problem.Title)
    //                .With(PathKey, "TODO");
    //
    ////            if (problem is ConstraintViolationProblem) {
    ////                builder
    //////                    .with(VIOLATIONS_KEY, ((ConstraintViolationProblem) problem).getViolations())
    ////                    .With(ViolationsKey, "")
    ////                    .With(MessageKey, ErrorConstants.ErrValidation);
    ////            }
    ////            else {
    //            builder
    //                .WithCause(problem.Cause)
    //                .WithDetail(problem.Detail)
    //                .WithInstance(problem.Instance);
    //            foreach (var (key, value) in problem.Parameters) builder.With(key, value);
    //
    //            if (!problem.Parameters.Keys.Contains(MessageKey)) builder.With(MessageKey, $"error.http.{problem.Status}");
    //
    ////            }
    //            return new ProblemResult(builder.Build(), result.Headers);
    //        }
    //
    //        [ExceptionHandler(typeof(BadRequestAlertException))]
    //        public ProblemResult HandleBadRequestAlertException(BadRequestAlertException exception)
    //        {
    //            //TODO redefine problem methods signatures
    //            return Create(exception, StatusCodes.Status400BadRequest, "Bad Request",
    //                HeaderUtil.CreateFailureAlert(exception.EntityName, exception.ErrorKey, exception.Message));
    //        }
    //
    //        [ExceptionHandler(typeof(DbUpdateConcurrencyException))]
    //        public ProblemResult HandleDbUpdateConcurrencyException(DbUpdateConcurrencyException exception)
    //        {
    //            return Create(exception);
    //        }
    //
    //        [ExceptionHandler(typeof(EmailNotFoundException))]
    //        public ProblemResult HandleEmailNotFoundException(EmailNotFoundException exception)
    //        {
    //            return Create(exception);
    //        }
    //
    //        [ExceptionHandler(typeof(InvalidPasswordException))]
    //        public ProblemResult HandleInvalidPasswordException(InvalidPasswordException exception)
    //        {
    //            return Create(exception);
    //        }
}

namespace MacsBusinessManagementAPI.Infrastructure.EntityValidator
{
    public class EntityValidationResult
    {
        public EntityValidationResult(bool success = false)
            => IsSuccess = success;

        public EntityValidationResult(string failureMessage)
            => ValidationMessage = failureMessage;

        public string ValidationMessage { get; init; }

        public bool IsSuccess { get; set; }

        public static EntityValidationResult Failure(string entityName, long id)
            => new($"{entityName} {id} could not be found.");

        public static EntityValidationResult Success()
            => new(true);
    }
}

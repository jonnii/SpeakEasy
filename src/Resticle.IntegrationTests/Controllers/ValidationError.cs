namespace HttpSpeak.IntegrationTests.Controllers
{
    public class ValidationError
    {
        public ValidationError()
        {
        }

        public ValidationError(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
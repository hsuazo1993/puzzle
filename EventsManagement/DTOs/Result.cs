namespace DTOs
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; } = default!;
        public string ErrorMessage { get; set; } = string.Empty;

        private Result() 
        {
            IsSuccess = true;
        }
        private Result(T data)
        {
            IsSuccess = true;
            Data = data;
        }
        private Result(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }


        public static Result<T> Success() => new Result<T>();
        public static Result<T> Success(T data)  => new Result<T>(data);
        public static Result<T> Failure(string errorMessage) => new Result<T>(errorMessage);
        public static Result<T> Failure(IEnumerable<string> errors)
        {
            string errorsJoined = string.Join(Environment.NewLine, errors);

            return new Result<T>(errorsJoined);
        }
    }

}

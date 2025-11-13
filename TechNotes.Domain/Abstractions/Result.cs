namespace TechNotes.Domain.Abstractions
{
    public class Result
    {
        public bool IsSuccesful { get; }
        public bool HasFailed => !IsSuccesful;
        public string? ErrorMessage { get; }

        public Result(bool isSuccesful, string? errorMessage = null)
        {
            IsSuccesful = isSuccesful;
            ErrorMessage = errorMessage;
        }

        public static Result Ok() => new Result(true);
        public static Result Fail(string errorMessage) => new Result(false, errorMessage);
        public static Result<T> Ok<T>(T value) => new(value,true,string.Empty);
        public static Result<T> Fail<T>(string errorMessage) => new(default, false, errorMessage);

        public static Result<T> FromValue<T>(T? value) => value is not null ? Ok(value) : Fail<T>("El valor no puede ser nulo");
    }

    public class Result<T> : Result
    {
        public T? Value { get; }
        protected internal Result(T? value, bool isSuccesful, string? errorMessage = null)
            : base(isSuccesful, errorMessage)
        {
            Value = value;
        }

        public static implicit operator Result<T>(T? value) => FromValue(value);
        public static implicit operator T?(Result<T> result) => result.Value;
    }
}

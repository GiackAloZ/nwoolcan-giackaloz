using System;

namespace nwoolcan.main.utils
{
    public class Result
    {
        public bool Success { get; }

        public bool Failure => !Success;

        public Exception Error { get; }

        protected Result(bool success, Exception error)
        {
            Success = success;
            Error = error;
        }
        
        protected Result(Exception error) : this(false, error) { }
        
        protected Result() : this(true, null) { }

        public Result Fail(string message)
        {
            return new Result(new Exception(message));
        }

        public Result Fail(Exception error)
        {
            return new Result(error);
        }

        public Result Ok()
        {
            return new Result();
        }

        public Result<T> Ok<T>(T value)
        {
            return new Result<T>(value);
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected internal Result(T value) : base()
        {
            Value = value;
        }

        protected internal Result(Exception error) : base(error)
        {
            Value = default(T);
        }
    }
}
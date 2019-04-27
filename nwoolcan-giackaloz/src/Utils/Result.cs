using System;

namespace NWoolcan.Utils
{
    public class Result
    {
        private const string NoErrorMessage = "There is no error for this result.";

        private readonly Exception _error;
        
        public bool Success { get; }
        
        public bool Failure => !Success;

        public Exception Error => Success ? throw new MissingMemberException(NoErrorMessage) : _error;

        protected Result(bool success, Exception error)
        {
            Success = success;
            _error = error;
        }

        public static Result Fail(Exception error)
        {
            return new Result(false, error);
        }

        public static Result Fail(string message)
        {
            return Fail(new Exception(message));
        }
        
        public Result Where(Func<bool> predicate, Func<Exception> errorGenerator)
        {
            if (Success)
            {
                return predicate() ? this : new Result(false, errorGenerator());
            }
            return this;
        }
        
        public Result Where(Func<bool> predicate, Exception error)
        {
            return Where(predicate, () => error);
        }
        
        public Result Where(Func<bool> predicate)
        {
            return Where(predicate, new Exception());
        }
        
        public Result Where(bool condition)
        {
            return Where(() => condition);
        }

        protected internal Result<T> ToValue<T>(T value)
        {
            return Success ? new Result<T>(value) : new Result<T>(Error);
        }
    }

    public class Result<T> : Result
    {
        private const string NoValueMessage = "There is no value for this result.";
        
        private readonly T _value;

        public T Value => Failure ? throw new MissingMemberException(NoValueMessage) : _value;

        protected internal Result(Exception error) : base(false, error) { }

        protected internal Result(T value) : base(value != null, value == null ? new Exception() : null)
        {
            _value = value;
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(value);
        }

        public new static Result<T> Fail(Exception error)
        {
            return Result.Fail(error).ToValue(default(T));
        }

        public new static Result<T> Fail(string message)
        {
            return Fail(new Exception(message));
        }
        
        public Result<T> Where(Predicate<T> predicate, Func<Exception> errorGenerator)
        {
            return base.Where(() => predicate(_value), errorGenerator)
                       .ToValue(_value);
        }

        public Result<T> Where(Predicate<T> predicate, Exception error)
        {
            return Where(predicate, () => error);
        }

        public Result<T> Where(Predicate<T> predicate, string message)
        {
            return Where(predicate, new ArgumentException(message));
        }

        public Result<T> Where(Predicate<T> predicate)
        {
            return Where(predicate, new Exception());
        }

        public Result<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return Success ? ToValue(selector(Value)) : Result<TResult>.Fail(Error);
        }

        public Result<TResult> FlatSelect<TResult>(Func<T, Result<TResult>> flatSelector)
        {
            return Success ? flatSelector(Value) : Result<TResult>.Fail(Error);
        }
    }
}
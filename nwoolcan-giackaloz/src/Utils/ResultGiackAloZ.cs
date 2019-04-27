using System;

namespace NWoolcan.Utils
{
    public class ResultGiackAloZ
    {
        private const string NoErrorMessage = "There is no error for this result.";

        private readonly Exception _error;
        
        public bool Success { get; }
        
        public bool Failure => !Success;

        public Exception Error => Success ? throw new MissingMemberException(NoErrorMessage) : _error;

        protected ResultGiackAloZ(bool success, Exception error)
        {
            Success = success;
            _error = error;
        }

        public static ResultGiackAloZ Fail(Exception error)
        {
            return new ResultGiackAloZ(false, error);
        }

        public static ResultGiackAloZ Fail(string message)
        {
            return Fail(new Exception(message));
        }
        
        public ResultGiackAloZ Where(Func<bool> predicate, Func<Exception> errorGenerator)
        {
            if (Success)
            {
                return predicate() ? this : new ResultGiackAloZ(false, errorGenerator());
            }
            return this;
        }
        
        public ResultGiackAloZ Where(Func<bool> predicate, Exception error)
        {
            return Where(predicate, () => error);
        }
        
        public ResultGiackAloZ Where(Func<bool> predicate)
        {
            return Where(predicate, new Exception());
        }
        
        public ResultGiackAloZ Where(bool condition)
        {
            return Where(() => condition);
        }

        protected internal ResultGiackAloZ<T> ToValue<T>(T value)
        {
            return Success ? new ResultGiackAloZ<T>(value) : new ResultGiackAloZ<T>(Error);
        }
    }

    public class ResultGiackAloZ<T> : ResultGiackAloZ
    {
        private const string NoValueMessage = "There is no value for this result.";
        
        private readonly T _value;

        public T Value => Failure ? throw new MissingMemberException(NoValueMessage) : _value;

        protected internal ResultGiackAloZ(Exception error) : base(false, error) { }

        protected internal ResultGiackAloZ(T value) : base(value != null, value == null ? new Exception() : null)
        {
            _value = value;
        }

        public static ResultGiackAloZ<T> Ok(T value)
        {
            return new ResultGiackAloZ<T>(value);
        }

        public new static ResultGiackAloZ<T> Fail(Exception error)
        {
            return ResultGiackAloZ.Fail(error).ToValue(default(T));
        }

        public new static ResultGiackAloZ<T> Fail(string message)
        {
            return Fail(new Exception(message));
        }
        
        public ResultGiackAloZ<T> Where(Predicate<T> predicate, Func<Exception> errorGenerator)
        {
            return base.Where(() => predicate(_value), errorGenerator)
                       .ToValue(_value);
        }

        public ResultGiackAloZ<T> Where(Predicate<T> predicate, Exception error)
        {
            return Where(predicate, () => error);
        }

        public ResultGiackAloZ<T> Where(Predicate<T> predicate, string message)
        {
            return Where(predicate, new ArgumentException(message));
        }

        public ResultGiackAloZ<T> Where(Predicate<T> predicate)
        {
            return Where(predicate, new Exception());
        }

        public ResultGiackAloZ<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return Success ? ToValue(selector(Value)) : ResultGiackAloZ<TResult>.Fail(Error);
        }

        public ResultGiackAloZ<TResult> FlatSelect<TResult>(Func<T, ResultGiackAloZ<TResult>> flatSelector)
        {
            return Success ? flatSelector(Value) : ResultGiackAloZ<TResult>.Fail(Error);
        }
    }
}
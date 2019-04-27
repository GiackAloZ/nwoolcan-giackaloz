using System;
using System.Data.SqlTypes;
using CSharpFunctionalExtensions;

namespace NWoolcan.Utils
{
    public class Quantity : IComparable<Quantity>, IEquatable<Quantity>
    {
        public double Value { get; }
        
        public UnitOfMeasure UnitOfMeasure { get; }

        private Quantity(double value, UnitOfMeasure unitOfMeasure)
        {
            Value = value;
            UnitOfMeasure = unitOfMeasure;
        }

        public static Result<Quantity> Of(double value, UnitOfMeasure unitOfMeasure)
        {
            return QuantityChecker.Check(new Quantity(value, unitOfMeasure));
        }

        public int CompareTo(Quantity other)
        {
            var valueComparison = Value.CompareTo(other.Value);
            return valueComparison != 0 ? valueComparison : UnitOfMeasure.CompareTo(other.UnitOfMeasure);
        }

        public bool Equals(Quantity other)
        {
            return Value.Equals(other.Value) && UnitOfMeasure == other.UnitOfMeasure;
        }

        public override bool Equals(object obj)
        {
            return obj.GetType() == this.GetType() && Equals((Quantity) obj);
        }

        public override int GetHashCode()
        {
            return (Value.GetHashCode() * 397) ^ (int) UnitOfMeasure;
        }

        public override string ToString()
        {
            return $"[Quantity]{nameof(Value)}: {Value}, {nameof(UnitOfMeasure)}: {UnitOfMeasure}";
        }
    }
}
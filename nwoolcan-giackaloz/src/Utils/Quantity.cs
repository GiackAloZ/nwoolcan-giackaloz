using System;

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
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var valueComparison = Value.CompareTo(other.Value);
            return valueComparison != 0 ? valueComparison : UnitOfMeasure.CompareTo(other.UnitOfMeasure);
        }

        public bool Equals(Quantity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value) && UnitOfMeasure == other.UnitOfMeasure;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
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
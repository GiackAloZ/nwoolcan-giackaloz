using System;

namespace NWoolcan.Utils
{
    public class Quantity
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
    }
}
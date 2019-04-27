using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using CSharpFunctionalExtensions;

namespace NWoolcan.Utils
{
    internal static class QuantityChecker
    {
        private static readonly IList<UnitOfMeasure> Valids = new List<UnitOfMeasure>
        {
            UnitOfMeasure.Gram,
            UnitOfMeasure.Liter,
            UnitOfMeasure.Bottle33Cl,
            UnitOfMeasure.Bottle50Cl,
            UnitOfMeasure.Bottle66Cl,
            UnitOfMeasure.Bottle75Cl,
            UnitOfMeasure.BottleMagnum
        };

        private const string NotValidUnitOfMeasureMessage = "Quantity unit of measure is not valid.";
        private const string NegativeValueMessage = "Quantity value is negative.";
        private const string NotValidValueMessage = "Quantity value is not valid.";

        internal static IReadOnlyList<UnitOfMeasure> GetValidUnitOfMeasures()
        {
            return new ReadOnlyCollection<UnitOfMeasure>(Valids);
        }

        internal static Result<Quantity> Check(Quantity quantity)
        {
            return Result.Ok(quantity)
                         .Ensure(q => Valids.Contains(q.UnitOfMeasure),
                             NotValidUnitOfMeasureMessage)
                         .Ensure(q => q.Value.IsPositive(),
                             NegativeValueMessage)
                         .Ensure(q => q.UnitOfMeasure.Validate(q.Value),
                             NotValidValueMessage);
        }
    }
}
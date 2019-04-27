using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NWoolcan.Utils
{
    static class QuantityChecker
    {
        private static IList<UnitOfMeasure> _valids = new List<UnitOfMeasure>
        {
            UnitOfMeasure.Gram,
            UnitOfMeasure.Liter,
            UnitOfMeasure.Bottle33Cl,
            UnitOfMeasure.Bottle50Cl,
            UnitOfMeasure.Bottle66Cl,
            UnitOfMeasure.Bottle75Cl,
            UnitOfMeasure.BottleMagnum
        };

        static IReadOnlyList<UnitOfMeasure> GetValidUnitOfMeasures()
        {
            return new ReadOnlyCollection<UnitOfMeasure>(_valids);
        }

        static bool Check(Quantity q)
        {
            throw new NotImplementedException();
        }
    }
}
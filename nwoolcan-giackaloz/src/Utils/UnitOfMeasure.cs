using System;
using System.Collections.Generic;

namespace NWoolcan.Utils
{
    public enum UnitOfMeasure
    {
        Liter, Gram, Bottle33Cl, Bottle50Cl, Bottle66Cl, Bottle75Cl, BottleMagnum
    }

    public static class UnitOfMeasureExtensions
    {
        internal static bool IsPositive(this double v) => v >= 0;

        internal static bool IsInteger(this double v) => v.Equals(Math.Floor(v));
        
        private static IDictionary<UnitOfMeasure, string> _symbols = new Dictionary<UnitOfMeasure, string>(
            new List<KeyValuePair<UnitOfMeasure, string>>
            {
                new KeyValuePair<UnitOfMeasure, string>(UnitOfMeasure.Liter, "L"),
                new KeyValuePair<UnitOfMeasure, string>(UnitOfMeasure.Gram, "g"),
                new KeyValuePair<UnitOfMeasure, string>(UnitOfMeasure.Bottle33Cl, "bot 33cl"),
                new KeyValuePair<UnitOfMeasure, string>(UnitOfMeasure.Bottle50Cl, "bot 50cl"),
                new KeyValuePair<UnitOfMeasure, string>(UnitOfMeasure.Bottle66Cl, "bot 66cl"),
                new KeyValuePair<UnitOfMeasure, string>(UnitOfMeasure.Bottle75Cl, "bot 75cl"),
                new KeyValuePair<UnitOfMeasure, string>(UnitOfMeasure.BottleMagnum, "bot magnum"),
            });
        
        private static IDictionary<UnitOfMeasure, Predicate<double>> _validators = new Dictionary<UnitOfMeasure, Predicate<double>>(
            new List<KeyValuePair<UnitOfMeasure, Predicate<double>>>
            {
                new KeyValuePair<UnitOfMeasure, Predicate<double>>(UnitOfMeasure.Liter, n => n.IsPositive()),
                new KeyValuePair<UnitOfMeasure, Predicate<double>>(UnitOfMeasure.Gram, n => n.IsPositive()),
                new KeyValuePair<UnitOfMeasure, Predicate<double>>(UnitOfMeasure.Bottle33Cl, n => n.IsPositive() && n.IsInteger()),
                new KeyValuePair<UnitOfMeasure, Predicate<double>>(UnitOfMeasure.Bottle50Cl, n => n.IsPositive() && n.IsInteger()),
                new KeyValuePair<UnitOfMeasure, Predicate<double>>(UnitOfMeasure.Bottle66Cl, n => n.IsPositive() && n.IsInteger()),
                new KeyValuePair<UnitOfMeasure, Predicate<double>>(UnitOfMeasure.Bottle75Cl, n => n.IsPositive() && n.IsInteger()),
                new KeyValuePair<UnitOfMeasure, Predicate<double>>(UnitOfMeasure.BottleMagnum, n => n.IsPositive() && n.IsInteger()),
            });

        public static string GetSymbol(this UnitOfMeasure uom) => _symbols[uom];

        public static bool Validate(this UnitOfMeasure uom, double value) => _validators[uom](value);
    }
}
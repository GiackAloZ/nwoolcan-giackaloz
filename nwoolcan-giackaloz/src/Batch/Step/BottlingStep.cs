using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using NWoolcan.Utils;

namespace NWoolcan.Batch.Step
{
    internal class BottlingStep : DecoratorStep
    {
        private static readonly ISet<UnitOfMeasure> Bottles = new HashSet<UnitOfMeasure>(
            new List<UnitOfMeasure>
            {
                UnitOfMeasure.Bottle33Cl,
                UnitOfMeasure.Bottle50Cl,
                UnitOfMeasure.Bottle66Cl,
                UnitOfMeasure.Bottle75Cl,
                UnitOfMeasure.BottleMagnum
            });
        
        public BottlingStep(Step decorated) : base(decorated) { }
        
        protected internal override Result CheckFinalizationData(DateTime endDate, Quantity endSize, string note)
        {
            return base.CheckFinalizationData(endDate, endSize, note)
                       .Ensure(() => Bottles.Contains(endSize.UnitOfMeasure),
                           "End size is not in a bottle unit of measure");
        }
    }
}
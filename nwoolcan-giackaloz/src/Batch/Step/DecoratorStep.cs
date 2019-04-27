using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using NWoolcan.Utils;

namespace NWoolcan.Batch.Step
{
    internal abstract class DecoratorStep : Step
    {
        private readonly Step _decorated;
        
        protected DecoratorStep(Step decorated) 
            : base(new FinalizableStepInfo(decorated.Info.Type, decorated.Info.StartDate),
                new HashSet<IStepType>(decorated.NextStepTypes))
        {
            _decorated = decorated;
        }

        protected internal override Result CheckFinalizationData(DateTime endDate, Quantity endSize, string note)
        {
            return _decorated.CheckFinalizationData(endDate, endSize, note);
        }
    }
}
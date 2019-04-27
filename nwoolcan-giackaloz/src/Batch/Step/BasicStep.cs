using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using NWoolcan.Utils;

namespace NWoolcan.Batch.Step
{
    internal class BasicStep : Step
    {
        internal BasicStep(
            IStepType stepType,
            DateTime startDate,
            ISet<IStepType> nextStepTypes) : base(new FinalizableStepInfo(stepType, startDate), nextStepTypes) { }

        protected internal override Result CheckFinalizationData(DateTime endDate, Quantity endSize, string note) => Result.Ok();
    }
}
using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using NWoolcan.Utils;

namespace NWoolcan.Batch.Step
{
    public interface IStep
    {
        IStepInfo Info { get; }
        
        IReadOnlyCollection<IStepType> NextStepTypes { get; }
        
        bool IsFinalized { get; }

        Result Finalize(DateTime endDate, Quantity endSize, string note = null);
    }
}
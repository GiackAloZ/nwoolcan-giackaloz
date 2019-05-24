using System.Collections.Generic;
using CSharpFunctionalExtensions;
using NWoolcan.Batch.Step;
using NWoolcan.Utils;

namespace NWoolcan.Batch
{
    public interface IBatch
    {
        int Id { get; }
        
        IBatchInfo Info { get; }

        IStep CurrentStep { get; }

        Quantity CurrentSize { get; }

        IEnumerable<IStep> Steps { get; }

        Result MoveToNextStep(IStepType nextStepType);

        bool Ended { get; }
    }
}
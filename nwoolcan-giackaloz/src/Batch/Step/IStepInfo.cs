using System;
using NWoolcan.Utils;
using Optional;

namespace NWoolcan.Batch.Step
{
    public interface IStepInfo
    {
        IStepType Type { get; }
        
        DateTime StartDate { get; }
        
        Option<string> Note { get; }
        
        Option<DateTime> EndDate { get; }
        
        Option<Quantity> EndSize { get; }
    }
}
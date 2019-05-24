using NWoolcan.Batch.Step;

namespace NWoolcan.Batch
{
    public interface IBatchMethod
    {
        string Name { get; }
        
        IStepType InitialStepType { get; }
    }
}
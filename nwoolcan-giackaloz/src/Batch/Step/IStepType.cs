namespace NWoolcan.Batch.Step
{
    public interface IStepType
    {
        string Name { get; }
        
        bool IsEndType { get; }
    }
}
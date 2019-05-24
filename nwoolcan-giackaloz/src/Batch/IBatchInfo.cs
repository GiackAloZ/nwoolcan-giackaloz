using NWoolcan.Utils;

namespace NWoolcan.Batch
{
    public interface IBatchInfo
    {
        IBatchMethod Method { get; }
        Quantity InitialSize { get; }
    }
}
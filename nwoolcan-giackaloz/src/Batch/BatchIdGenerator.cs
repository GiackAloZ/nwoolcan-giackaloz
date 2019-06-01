using NWoolcan.Utils;

namespace NWoolcan.Batch
{
    public class BatchIdGenerator : IIdGenerator
    {
        private int _id = 1;

        public int NextId => _id++;
    }
}
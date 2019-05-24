using System;
using System.Collections.Generic;
using NWoolcan.Batch.Step;

namespace NWoolcan.Batch
{
    public enum BatchMethodEnum
    {
        AllGrain, PartialMesh, Extract
    }

    public static class BatchMethodEnumExtensions
    {
        private static IDictionary<BatchMethodEnum, string> _names = new Dictionary<BatchMethodEnum, string>(
            new List<KeyValuePair<BatchMethodEnum, string>>
            {
                new KeyValuePair<BatchMethodEnum, string>(BatchMethodEnum.AllGrain, "All grain"),
                new KeyValuePair<BatchMethodEnum, string>(BatchMethodEnum.PartialMesh, "Partial Mesh"),
                new KeyValuePair<BatchMethodEnum, string>(BatchMethodEnum.Extract, "Extract"),
            });
        
        private static IDictionary<BatchMethodEnum, IStepType> _stepTypes = new Dictionary<BatchMethodEnum, IStepType>(
            new List<KeyValuePair<BatchMethodEnum, IStepType>>
            {
                new KeyValuePair<BatchMethodEnum, IStepType>(BatchMethodEnum.AllGrain, StepTypeEnum.Mashing.ToStepType()),
                new KeyValuePair<BatchMethodEnum, IStepType>(BatchMethodEnum.PartialMesh, StepTypeEnum.Mashing.ToStepType()),
                new KeyValuePair<BatchMethodEnum, IStepType>(BatchMethodEnum.Extract, StepTypeEnum.Boiling.ToStepType()),
            });

        private class BatchMethodContainer : IBatchMethod
        {
            public string Name { get; set;  }
            public IStepType InitialStepType { get; set; }
        }
        
        public static IBatchMethod ToBatchMethod(this BatchMethodEnum method)
        {
            return new BatchMethodContainer
            {
                Name = _names[method],
                InitialStepType = _stepTypes[method]
            };
        }
    }
}
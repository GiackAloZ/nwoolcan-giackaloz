namespace NWoolcan.Batch.Step
{
    public enum StepTypeEnum
    {
        Mashing, Boiling, Fermenting, Aging, Packaging, Finalized
    }
    
    public static class StepTypeEnumExtensions {
        
        private class StepType : IStepType
        {
            public string Name { get; internal set; }
            public bool IsEndType { get; internal set; }
        }

        public static IStepType ToStepType(this StepTypeEnum type)
        {
            return new StepType
            {
                Name = type.ToString(),
                IsEndType = type.Equals(StepTypeEnum.Finalized)
            };
        }
    }
}
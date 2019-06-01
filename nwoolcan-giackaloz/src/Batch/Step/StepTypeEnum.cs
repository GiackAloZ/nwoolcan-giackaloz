using System;

namespace NWoolcan.Batch.Step
{
    public enum StepTypeEnum
    {
        Mashing, Boiling, Fermenting, Aging, Packaging, Finalized
    }
    
    public static class StepTypeEnumExtensions {
        
        private class StepType : IStepType, IEquatable<IStepType>
        {
            public string Name { get; internal set; }
            public bool IsEndType { get; internal set; }

            public bool Equals(IStepType other)
            {
                return string.Equals(Name, other.Name) && IsEndType == other.IsEndType;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((StepType) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ IsEndType.GetHashCode();
                }
            }

            public static bool operator ==(StepType left, StepType right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(StepType left, StepType right)
            {
                return !Equals(left, right);
            }
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
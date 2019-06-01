using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace NWoolcan.Batch.Step
{
    public class EnumStepFactory : IStepFactory
    {
        private const string CannotFindStepImplementationMessage = " does not have a configured implementation.";

        private static readonly ISet<IStepType> MashingStepTypes = new HashSet<IStepType>(
            new List<IStepType>
            {
                StepTypeEnum.Boiling.ToStepType()
            }
            );
        private static readonly ISet<IStepType> BoilingStepTypes = new HashSet<IStepType>(
            new List<IStepType>
            {
                StepTypeEnum.Fermenting.ToStepType()
            }
        );
        private static readonly ISet<IStepType> FermentingStepTypes = new HashSet<IStepType>(
            new List<IStepType>
            {
                StepTypeEnum.Packaging.ToStepType(),
                StepTypeEnum.Aging.ToStepType()
            }
        );
        private static readonly ISet<IStepType> AgingStepTypes = new HashSet<IStepType>(
            new List<IStepType>
            {
                StepTypeEnum.Packaging.ToStepType()
            }
        );
        private static readonly ISet<IStepType> PackagingStepTypes = new HashSet<IStepType>(
            new List<IStepType>
            {
                StepTypeEnum.Finalized.ToStepType()
            }
        );
        
        public Result<IStep> Create(IStepType type, DateTime startDate)
        {
            return Result.Ok(type)
                         .Ensure(st => st.GetType() == StepTypeEnum.Aging.ToStepType().GetType(),
                             type.ToString() + CannotFindStepImplementationMessage)
                         .Map<IStepType, IStep>(st =>
                         {
                             if (st.Equals(StepTypeEnum.Mashing.ToStepType()))
                             {
                                 return new BasicStep(st, startDate, MashingStepTypes);
                             }
                             else if (st.Equals(StepTypeEnum.Boiling.ToStepType()))
                             {
                                 return new BasicStep(st, startDate, BoilingStepTypes);
                             }
                             else if (st.Equals(StepTypeEnum.Fermenting.ToStepType()))
                             {
                                 return new BasicStep(st, startDate, FermentingStepTypes);
                             }
                             else if (st.Equals(StepTypeEnum.Aging.ToStepType()))
                             {
                                 return new BasicStep(st, startDate, AgingStepTypes);
                             }
                             else if (st.Equals(StepTypeEnum.Packaging.ToStepType()))
                             {
                                 return new BottlingStep(new BasicStep(st, startDate, PackagingStepTypes));
                             }
                             return new BasicStep(st, startDate, new HashSet<IStepType>());
                         });
        }
    }
}
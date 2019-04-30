using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CSharpFunctionalExtensions;
using NWoolcan.Utils;
using Optional;

namespace NWoolcan.Batch.Step
{
    internal abstract class Step : IStep
    {
        protected class FinalizableStepInfo : IStepInfo
        {
            public IStepType Type { get; }
            public DateTime StartDate { get; }

            public FinalizableStepInfo(IStepType stepType, DateTime startDate)
            {
                Type = stepType;
                StartDate = startDate;
            }
            
            public Option<string> Note { get; private set; }
            public Option<DateTime> EndDate { get; private set; }
            public Option<Quantity> EndSize { get; private set; }

            internal Result Finalize(DateTime endDate, Quantity endSize, string note)
            {
                return Result.Ok()
                             .Ensure(() => endDate.CompareTo(StartDate) >= 0,
                                 "End date is before start date.")
                             .OnSuccess(() =>
                             {
                                 Note = Option.Some(note);
                                 EndDate = Option.Some(endDate);
                                 EndSize = Option.Some(endSize);
                             });
            }
        }

        private readonly FinalizableStepInfo _stepInfo;

        public IStepInfo Info => _stepInfo;

        public IReadOnlyCollection<IStepType> NextStepTypes { get; }
        
        public bool IsFinalized { get; }

        protected Step(FinalizableStepInfo stepInfo, ISet<IStepType> nextStepTypes)
        {
            _stepInfo = stepInfo;
            NextStepTypes = nextStepTypes.ToList().AsReadOnly();
            IsFinalized = stepInfo.Type.IsEndType;
        }

        protected internal abstract Result CheckFinalizationData(DateTime endDate, Quantity endSize, string note);
        
        public Result Finalize(DateTime endDate, Quantity endSize, string note = null)
        {
            return Result.Ok()
                         .Ensure(() => !IsFinalized, "Cannot finalize step because is already finalized.")
                         .OnSuccess(() => CheckFinalizationData(endDate, endSize, note))
                         .OnSuccess(() => _stepInfo.Finalize(endDate, endSize, note));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using NWoolcan.Batch.Step;
using NWoolcan.Utils;
using Optional.Unsafe;

namespace NWoolcan.Batch
{
    public class Batch : IBatch
    {
        private const string CannotGoToNextStepMessage = "From this step, cannot go to step: ";

        private class BatchInfo : IBatchInfo
        {
            public IBatchMethod Method { get; set; }
            public Quantity InitialSize { get; set; }
        }
        
        private readonly List<IStep> _steps;
        private readonly IStepFactory _stepFactory;

        public int Id { get; private set; }

        public IBatchInfo Info { get; private set; }

        public IStep CurrentStep => _steps.Last();

        public Quantity CurrentSize => _steps.SkipLast(1).Last().Info.EndSize.ValueOrDefault() ?? Info.InitialSize;

        public IEnumerable<IStep> Steps => _steps.AsReadOnly();

        public bool Ended => CurrentStep.Info.Type.IsEndType;

        public Batch(IBatchMethod method, Quantity initialSize, IIdGenerator idGenerator, IStepFactory stepFactory)
        {
            _stepFactory = stepFactory;
            _steps = new List<IStep>();

            Action cannotCreateInitialStep = () => throw new ArgumentException();

            _stepFactory.Create(method.InitialStepType, DateTime.Now)
                        .OnFailure(cannotCreateInitialStep)
                        .OnSuccess(step => _steps.Add(step));

            Info = new BatchInfo
            {
                Method = method,
                InitialSize = initialSize
            };
            Id = idGenerator.NextId;
        }

        public Result MoveToNextStep(IStepType nextStepType)
        {
            return Result.Ok(CurrentStep)
                         .Ensure(step => step.NextStepTypes.Contains(nextStepType),
                             CannotGoToNextStepMessage + nextStepType.Name)
                         .OnSuccess(step => step.IsFinalized ? Result.Ok() : step.Finalize(DateTime.Now, CurrentSize))
                         .OnSuccess(() => _stepFactory.Create(nextStepType, DateTime.Now))
                         .OnSuccess(nextStep => _steps.Add(nextStep));
        }
    }
}
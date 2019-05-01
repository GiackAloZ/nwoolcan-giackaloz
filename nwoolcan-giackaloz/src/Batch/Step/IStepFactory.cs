using System;
using CSharpFunctionalExtensions;

namespace NWoolcan.Batch.Step
{
    public interface IStepFactory
    {
        Result<IStep> Create(IStepType type, DateTime startDate);
    }
}
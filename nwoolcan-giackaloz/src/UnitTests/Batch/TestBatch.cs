using System;
using System.Linq;
using CSharpFunctionalExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NWoolcan.Batch;
using NWoolcan.Batch.Step;
using NWoolcan.Utils;

namespace NWoolcan.UnitTests.Batch
{
    [TestClass]
    public class TestBatch
    {
        
        private IIdGenerator _gen = new BatchIdGenerator();
        private IStepFactory _stepFactory = new EnumStepFactory();

        private Quantity Q1 = Quantity.Of(1000, UnitOfMeasure.Liter).Value;
        private Quantity Q2 = Quantity.Of(2000, UnitOfMeasure.Liter).Value;
        
        private IBatch _batchAlfredo;
        private IBatch _batchRossina;
        private IBatch _batchBiondina;

        [TestInitialize]
        public void Init()
        {
            _batchAlfredo = new NWoolcan.Batch.Batch(BatchMethodEnum.AllGrain.ToBatchMethod(),
                Q1,
                _gen,
                _stepFactory);
            _batchRossina = new NWoolcan.Batch.Batch(BatchMethodEnum.PartialMesh.ToBatchMethod(),
                Q1,
                _gen,
                _stepFactory);
            _batchBiondina = new NWoolcan.Batch.Batch(BatchMethodEnum.Extract.ToBatchMethod(),
                Q2,
                _gen,
                _stepFactory);
        }

        [TestMethod]
        public void TestInit()
        {   
            Assert.AreNotEqual(_batchAlfredo.Id, _batchRossina.Id);

            Assert.AreEqual(StepTypeEnum.Mashing.ToStepType(), _batchAlfredo.CurrentStep.Info.Type);
            Assert.AreEqual(StepTypeEnum.Mashing.ToStepType(), _batchRossina.CurrentStep.Info.Type);
            Assert.AreEqual(StepTypeEnum.Boiling.ToStepType(), _batchBiondina.CurrentStep.Info.Type);
            
            Assert.AreEqual(Q1, _batchAlfredo.CurrentSize);
            Assert.IsFalse(_batchAlfredo.Ended);
        }

        [TestMethod]
        public void TestChangeStep()
        {
            // No finalization
            var res = _batchAlfredo.MoveToNextStep(StepTypeEnum.Boiling.ToStepType());
            Assert.IsTrue(res.IsSuccess);
            
            Assert.AreEqual(StepTypeEnum.Boiling.ToStepType(), _batchAlfredo.CurrentStep.Info.Type);
            Assert.AreEqual(Q1, _batchAlfredo.CurrentSize);

            var prevStep = _batchAlfredo.Steps.First();
            Assert.IsTrue(prevStep.IsFinalized);
            Assert.AreEqual(StepTypeEnum.Mashing.ToStepType(), prevStep.Info.Type);

            // With finalization

            _batchRossina.CurrentStep.Finalize(DateTime.Now, Q2, "dummy notes");
            res = _batchRossina.MoveToNextStep(StepTypeEnum.Boiling.ToStepType());
            Assert.IsTrue(res.IsSuccess);
            
            Assert.AreNotEqual(Q2, _batchRossina.Info.InitialSize);
            Assert.AreEqual(Q2, _batchRossina.CurrentSize);
            
            // Wrong step change
            res = _batchBiondina.MoveToNextStep(StepTypeEnum.Boiling.ToStepType());
            Assert.IsTrue(res.IsFailure);
        }

        [TestMethod]
        public void TestCompleteStepChange()
        {
            _batchAlfredo.MoveToNextStep(StepTypeEnum.Boiling.ToStepType()).OnFailure(() => Assert.Fail());
            _batchAlfredo.MoveToNextStep(StepTypeEnum.Fermenting.ToStepType()).OnFailure(() => Assert.Fail());
            _batchAlfredo.MoveToNextStep(StepTypeEnum.Packaging.ToStepType()).OnFailure(() => Assert.Fail());
            
            _batchAlfredo.MoveToNextStep(StepTypeEnum.Finalized.ToStepType()).OnSuccess(() => Assert.Fail());
            
            _batchAlfredo.CurrentStep.Finalize(DateTime.Now, Quantity.Of(10, UnitOfMeasure.Bottle33Cl).Value);

            _batchAlfredo.MoveToNextStep(StepTypeEnum.Finalized.ToStepType()).OnFailure(() => Assert.Fail());
            
            Assert.IsTrue(_batchAlfredo.Ended);
        }
    }
}
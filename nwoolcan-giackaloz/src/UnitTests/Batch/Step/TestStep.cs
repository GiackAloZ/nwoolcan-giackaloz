using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NWoolcan.Batch.Step;
using NWoolcan.Utils;
using Optional.Unsafe;

namespace NWoolcan.UnitTests.Batch.Step
{
    [TestClass]
    public class TestStep
    {
        
        private IStepFactory _factory = new EnumStepFactory();

        private static Quantity Q1 = Quantity.Of(10, UnitOfMeasure.Liter).Value;

        private IStep _mashing;
        private IStep _boiling;
        private IStep _packaging;
        private IStep _finalized;

        [TestInitialize]
        public void Init()
        {
            _mashing = _factory.Create(StepTypeEnum.Mashing.ToStepType(), DateTime.Now).Value;
            _boiling = _factory.Create(StepTypeEnum.Boiling.ToStepType(), DateTime.Now).Value;
            _packaging = _factory.Create(StepTypeEnum.Packaging.ToStepType(), DateTime.Now).Value;
            _finalized = _factory.Create(StepTypeEnum.Finalized.ToStepType(), DateTime.Now).Value;
        }

        [TestMethod]
        public void TestFinalization()
        {
            Assert.IsFalse(_mashing.IsFinalized);
            
            var endDate = DateTime.Now;
            var res = _mashing.Finalize(endDate, Q1, "Finalized");
            Assert.IsTrue(res.IsSuccess);
            Assert.IsTrue(_mashing.IsFinalized);
            
            // Again
            res = _mashing.Finalize(endDate, Q1, "again");
            Assert.IsTrue(res.IsFailure);
            
            // Finalize before start date
            var yesterday = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            res = _boiling.Finalize(yesterday, Q1, "yesterday");
            Assert.IsTrue(res.IsFailure);
            
            // Check proprieties
            Assert.IsTrue(_mashing.Info.Note.HasValue);
            Assert.IsTrue(_mashing.Info.EndDate.HasValue);
            Assert.IsTrue(_mashing.Info.EndSize.HasValue);
            Assert.AreEqual("Finalized",_mashing.Info.Note.ValueOrDefault());
            Assert.AreEqual(endDate, _mashing.Info.EndDate.ValueOrDefault());
            Assert.AreEqual(Q1, _mashing.Info.EndSize.ValueOrDefault());
            
            // Cannot package with liters
            res = _packaging.Finalize(endDate, Q1, "no bottles");
            Assert.IsTrue(res.IsFailure);

            res = _packaging.Finalize(endDate, Quantity.Of(10, UnitOfMeasure.Bottle33Cl).Value);
            Assert.IsTrue(res.IsSuccess);
            
            // Cannot finalize finalized step
            res = _finalized.Finalize(endDate, Q1);
            Assert.IsTrue(res.IsFailure);
        }
    }
}
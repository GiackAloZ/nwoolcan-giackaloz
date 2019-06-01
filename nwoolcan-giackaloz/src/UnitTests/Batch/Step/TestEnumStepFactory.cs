using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NWoolcan.Batch.Step;

namespace NWoolcan.UnitTests.Batch.Step
{
    [TestClass]
    public class TestEnumStepFactory
    {
        
        private IStepFactory _factory = new EnumStepFactory();

        [TestMethod]
        public void SimpleStepCreation()
        {
            var now = DateTime.Now;
            var step = _factory.Create(StepTypeEnum.Aging.ToStepType(), now);
            
            Assert.IsTrue(step.IsSuccess);
            Assert.AreEqual(StepTypeEnum.Aging.ToStepType(), step.Value.Info.Type);
            Assert.AreEqual(now, step.Value.Info.StartDate);
        }
    }
}
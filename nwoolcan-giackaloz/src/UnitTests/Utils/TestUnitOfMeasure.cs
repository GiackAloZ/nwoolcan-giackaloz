using CSharpFunctionalExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NWoolcan.Utils;
using Result = CSharpFunctionalExtensions.Result;

namespace NWoolcan.UnitTests.Utils
{
    [TestClass]
    public class TestUnitOfMeasure
    {

        [TestMethod]
        public void Symbols()
        {
            Assert.AreEqual("L", UnitOfMeasure.Liter.GetSymbol());
        }

        [TestMethod]
        public void Validations()
        {
            Assert.IsTrue(UnitOfMeasure.Liter.Validate(10));
            Assert.IsTrue(UnitOfMeasure.Liter.Validate(10.1));
            Assert.IsTrue(UnitOfMeasure.Liter.Validate(0));
            Assert.IsFalse(UnitOfMeasure.Liter.Validate(-10));
            
            Assert.IsTrue(UnitOfMeasure.Gram.Validate(10));
            Assert.IsTrue(UnitOfMeasure.Gram.Validate(10.1));
            Assert.IsTrue(UnitOfMeasure.Gram.Validate(0));
            Assert.IsFalse(UnitOfMeasure.Gram.Validate(-10));
            
            Assert.IsTrue(UnitOfMeasure.Bottle33Cl.Validate(10));
            Assert.IsFalse(UnitOfMeasure.Bottle33Cl.Validate(10.1));
            Assert.IsTrue(UnitOfMeasure.Bottle33Cl.Validate(0));
            Assert.IsFalse(UnitOfMeasure.Bottle33Cl.Validate(-10));
            
            Assert.IsTrue(UnitOfMeasure.Bottle50Cl.Validate(10));
            Assert.IsFalse(UnitOfMeasure.Bottle50Cl.Validate(10.1));
            Assert.IsTrue(UnitOfMeasure.Bottle50Cl.Validate(0));
            Assert.IsFalse(UnitOfMeasure.Bottle50Cl.Validate(-10));
            
            Assert.IsTrue(UnitOfMeasure.Bottle66Cl.Validate(10));
            Assert.IsFalse(UnitOfMeasure.Bottle66Cl.Validate(10.1));
            Assert.IsTrue(UnitOfMeasure.Bottle66Cl.Validate(0));
            Assert.IsFalse(UnitOfMeasure.Bottle66Cl.Validate(-10));
            
            Assert.IsTrue(UnitOfMeasure.Bottle75Cl.Validate(10));
            Assert.IsFalse(UnitOfMeasure.Bottle75Cl.Validate(10.1));
            Assert.IsTrue(UnitOfMeasure.Bottle75Cl.Validate(0));
            Assert.IsFalse(UnitOfMeasure.Bottle75Cl.Validate(-10));
            
            Assert.IsTrue(UnitOfMeasure.BottleMagnum.Validate(10));
            Assert.IsFalse(UnitOfMeasure.BottleMagnum.Validate(10.1));
            Assert.IsTrue(UnitOfMeasure.BottleMagnum.Validate(0));
            Assert.IsFalse(UnitOfMeasure.BottleMagnum.Validate(-10));
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NWoolcan.Utils;

namespace NWoolcan.UnitTests.Utils
{
    [TestClass]
    public class TestQuantity
    {

        [TestMethod]
        public void SimpleCreation()
        {
            var q1 = Quantity.Of(1, UnitOfMeasure.Gram).Value;
            var q2 = Quantity.Of(0, UnitOfMeasure.Liter).Value;
            var q3 = Quantity.Of(1.9, UnitOfMeasure.Gram).Value;
            var q4 = Quantity.Of(10, UnitOfMeasure.Bottle66Cl).Value;
            
            Assert.AreEqual(1, q1.Value);
            Assert.AreEqual(UnitOfMeasure.Gram, q1.UnitOfMeasure);
            
            Assert.AreEqual(0, q2.Value);
            Assert.AreEqual(UnitOfMeasure.Liter, q2.UnitOfMeasure);
            
            Assert.AreEqual(1.9, q3.Value);
            Assert.AreEqual(UnitOfMeasure.Gram, q3.UnitOfMeasure);
            
            Assert.AreEqual(10, q4.Value);
            Assert.AreEqual(UnitOfMeasure.Bottle66Cl, q4.UnitOfMeasure);
        }

        [TestMethod]
        public void WrongCreation()
        {
            var res = Quantity.Of(-9, UnitOfMeasure.Gram);
            Assert.IsTrue(res.IsFailure);

            res = Quantity.Of(8.8, UnitOfMeasure.Bottle33Cl);
            Assert.IsTrue(res.IsFailure);
        }

        [TestMethod]
        public void Compare()
        {
            var q1 = Quantity.Of(1, UnitOfMeasure.Gram).Value;
            var q2 = Quantity.Of(2, UnitOfMeasure.Gram).Value;
            var q3 = Quantity.Of(1, UnitOfMeasure.Liter).Value;
            
            Assert.IsTrue(q1.CompareTo(q2) < 0);
            Assert.IsTrue(q1.CompareTo(q1) == 0);
            Assert.IsTrue(q2.CompareTo(q1) > 0);
            
            Assert.IsTrue(q1.CompareTo(q3) != q3.CompareTo(q1));
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NWoolcan.Utils;

namespace NWoolcan.UnitTests.Utils
{
    [TestClass]
    public class TestResult
    {

        [TestMethod]
        public void SimpleCreation()
        {
            var fail1 = Result.Fail("failed");
            var fail2 = Result.Fail(new ArithmeticException());
            var ok1 = Result<int>.Ok(10);
            var ok2 = Result<object>.Ok(new object());

            Assert.IsTrue(fail1.Failure);
            Assert.IsTrue(fail2.Failure);
            Assert.IsTrue(ok1.Success);
            Assert.AreEqual(10, ok1.Value);
            Assert.IsTrue(ok2.Success);
        }

        [TestMethod]
        public void ThrowsWhenNoError()
        {
            var ok = Result<int>.Ok(10);
            Assert.ThrowsException<MissingMemberException>(() => ok.Error);
        }
        
        [TestMethod]
        public void ThrowsWhenNoValue()
        {
            var fail = Result<int>.Fail("failed");
            Assert.ThrowsException<MissingMemberException>(() => fail.Value);
        }

        [TestMethod]
        public void Where()
        {
            var res = Result<int>.Ok(10);
            Assert.IsTrue(res.Where(false).Failure);
            Assert.IsTrue(res.Where(true).Success);
            Assert.IsTrue(res.Where(i => i == 0).Failure);
            Assert.IsTrue(res.Where(i => i == 10).Success);
        }

        [TestMethod]
        public void Select()
        {
            var res = Result<int>.Ok(10);
            var res2 = res.Select(i => i.ToString());
            Assert.IsTrue(res2.Success);
            Assert.AreEqual("10", res2.Value);
            
            res = Result<int>.Fail("failed");
            res2 = res.Select(i => i.ToString());
            Assert.IsTrue(res2.Failure);
        }

        [TestMethod]
        public void FlatSelect()
        {
            var res = Result<int>.Ok(10);
            var res2 = Result<string>.Ok("test");
            var res3 = res.FlatSelect(i => res2.Select(s => s + i.ToString()));
            
            Assert.IsTrue(res3.Success);
            Assert.AreEqual("test10", res3.Value);

            var failed = Result<string>.Fail("failed");
            res3 = res.FlatSelect(i => failed.Select(s => s + i.ToString()));
            
            Assert.IsTrue(res3.Failure);
        }

        [TestMethod]
        public void Laziness()
        {
            var fail = Result<int>.Fail("failed");

            fail = fail.Where(i =>
            {
                Assert.Fail();
                return true;
            });
            Assert.IsTrue(fail.Failure);
            
            fail = fail.Select(i =>
            {
                Assert.Fail();
                return i;
            });
            Assert.IsTrue(fail.Failure);

            fail = fail.FlatSelect(i =>
            {
                Assert.Fail();
                return Result<int>.Fail("failed");
            });
        }
    }
}
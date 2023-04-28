using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WindowsAPI.ExtendedLinguisticServicesWrapper;
using WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses;

namespace GenericTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ELS.GetServices();
        }
    }
}
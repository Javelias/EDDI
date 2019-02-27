﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class CoriolisTests : TestBase
    {
        [TestInitialize]
        public void start()
        {
            MakeSafe();
        }

        [TestMethod]
        public void TestUri()
        {
            string data = "BZ+24 123";
            string uriData = Uri.EscapeDataString(data);
            Assert.AreEqual("BZ%2B24%20123", uriData);
        }
    }
}

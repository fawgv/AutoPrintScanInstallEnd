using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrinterScannerAutoInstall;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestVerifyArgsClass
    {

        public static void GetIPFromArgsTest(string verifyResult, params string[] args)
        {
            // Arrange:
            //var curArgs = args;
            VerifyArgsClass verifyArgsClass = new VerifyArgsClass();

            //Act:
            var result = verifyArgsClass.GetIPFromArgs(args);

            //Assert:
            Assert.AreEqual(verifyResult, result);
        }

        /// <summary>
        /// Тест получения IP адреса из аргументов
        /// </summary>
        [TestMethod]
        public void TestMethodGetIPFromArgs1()
        {
            GetIPFromArgsTest("172.16.8.242", "/i", "172.16.8.242");
        }

        /// <summary>
        /// Тест получения IP адреса из аргументов
        /// </summary>
        [TestMethod]
        public void TestMethodGetIPFromArgs2()
        {
            GetIPFromArgsTest(string.Empty, "/i", "/i", ".");
        }

        /// <summary>
        /// Тест получения IP адреса из аргументов
        /// </summary>
        [TestMethod]
        public void TestMethodGetIPFromArgs3()
        {
            GetIPFromArgsTest(string.Empty, "/i", ".");
        }

        /// <summary>
        /// Тест получения IP адреса из аргументов
        /// </summary>
        [TestMethod]
        public void TestMethodGetIPFromArgs4()
        {
            GetIPFromArgsTest(string.Empty, null);
        }
    }
}

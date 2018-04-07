using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JSExec.Library;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace JSExec.Tests
{
    [TestClass]
    public class ScriptRunTest
    {
        private JavascriptExecutor _javascriptExecutor;

        public ScriptRunTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.google.com/";

            _javascriptExecutor = JavascriptExecutor.Create(new JavascriptRuntime((IJavaScriptExecutor)driver));
        }

        [TestMethod]
        public void TestDefine()
        {
            dynamic js = _javascriptExecutor.GetDynamicJs();
            js.test = "test";
            js.test1 = "test" + "-" + js.test.ExecuteReturn();
            js.test2 = 10;

            Assert.AreEqual(js.test.ExecuteReturn(), "test");
            Assert.AreEqual(js.test1.ExecuteReturn(), "test-test");
            Assert.AreEqual(js.test2.ExecuteReturn(), 10);
        }

        [TestMethod]
        public void TestBinaryOperations()
        {
            dynamic js = _javascriptExecutor.GetDynamicJs();
            js.number1 = 10;
            js.number2 = 20;

            long add = js.number1 + js.number2;
            long multiply = js.number1 * js.number2;
            long subtract = js.number2 - js.number1;
            long divide = js.number2 / js.number1;

            Assert.AreEqual(add, 30);
            Assert.AreEqual(multiply, 200);
            Assert.AreEqual(subtract, 10);
            Assert.AreEqual(divide, 2);
        }
    }
}

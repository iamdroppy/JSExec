using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JSExec.Library;

namespace JSExec.Tests
{
    [TestClass]
    public class ScriptParseTest
    {
        private JavascriptExecutor _javascriptExecutor;

        public ScriptParseTest()
        {
            _javascriptExecutor = JavascriptExecutor.Create(new JavascriptRawRuntime());
        }

        [TestMethod]
        public void TestScriptCreation()
        {
            dynamic js = _javascriptExecutor.GetDynamicJs();
            Assert.AreEqual(js.a.a.a().a(1, 2).a("1", 1, true)[10, "10"].ToString(), "a.a.a().a(1, 2).a('1', 1, True)[10, '10']");
        }
    }
}

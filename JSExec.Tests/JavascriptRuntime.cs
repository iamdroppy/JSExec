using JSExec.Library.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSExec.Tests
{
    public class JavascriptRuntime : IJavascriptRuntime
    {
        private IJavaScriptExecutor _jsExecutor;

        internal JavascriptRuntime(IJavaScriptExecutor jsExecutor)
        {
            _jsExecutor = jsExecutor;
        }

        public void ExecuteJavascript(string command)
        {
            _jsExecutor.ExecuteScript(command + ";");
        }

        public object ExecuteJavascriptReturn(string command)
        {
            return _jsExecutor.ExecuteScript("return " + command + ";");
        }

        public void SetJavascriptData(string data, object value)
        {
            _jsExecutor.ExecuteScript(data + " = " + value + ";");
        }
    }
}
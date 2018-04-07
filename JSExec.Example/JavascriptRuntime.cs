using JSExec.Library.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSExec.Example
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
            _jsExecutor.ExecuteScript(command);
        }

        public T ExecuteJavascriptReturn<T>(string command)
        {
           return (T)_jsExecutor.ExecuteScript("return " + command);
        }
    }
}

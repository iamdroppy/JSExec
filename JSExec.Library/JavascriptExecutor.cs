using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using JSExec.Library.Interfaces;

namespace JSExec.Library
{
    public class JavascriptExecutor
    {
        private IJavascriptRuntime _runtimeDriver;

        private JavascriptExecutor(IJavascriptRuntime runTimeDriver)
        {
            _runtimeDriver = runTimeDriver;
        }

        public static JavascriptExecutor Create(IJavascriptRuntime runtimeDriver) => new JavascriptExecutor(runtimeDriver);

        public dynamic GetDynamicJs() => new JavascriptDynamic(_runtimeDriver);
    }
}

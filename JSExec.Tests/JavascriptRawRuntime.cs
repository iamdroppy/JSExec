using JSExec.Library.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSExec.Tests
{
    public class JavascriptRawRuntime : IJavascriptRuntime
    {
        public void ExecuteJavascript(string command)
        { }

        public object ExecuteJavascriptReturn(string command)
        {
            return command;
        }

        public void SetJavascriptData(string data, object value)
        { }
    }
}

using JSExec.Library.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSExec.Example
{
    public class JavascriptRawRuntime : IJavascriptRuntime
    {
        public void ExecuteJavascript(string command)
        {
            Console.WriteLine("Executing: " + command);
        }

        public object ExecuteJavascriptReturn(string command)
        {
            Console.WriteLine("Executing: return " + command);
            return null;
        }

        public void SetJavascriptData(string data, object value)
        {
            Console.WriteLine("Executing: " + data + " = " + value);
        }
    }
}

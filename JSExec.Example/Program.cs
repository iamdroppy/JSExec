using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using JSExec.Library;
using OpenQA.Selenium.Chrome;

namespace JSExec.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // we build a ChromeDriver instance
            //IWebDriver driver = new ChromeDriver();

            // then we build a JavascriptExecutor instance, where it'll manage and create the dynamic
            // object. 
            var jsExecutor = JavascriptExecutor.Create(new JavascriptRuntime(null));

            dynamic js = jsExecutor.GetDynamicJs();
            js.alert("Hello world").Execute();
            js.alert("Hello world").Fuck("fs").Execute();

            // Done.
            Console.ReadKey();
        }
    }
}

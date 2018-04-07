using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using JSExec.Library;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace JSExec.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // we build a ChromeDriver instance
            ChromeDriver driver = new ChromeDriver();
            driver.Url = "https://www.google.com/";

            //IWebDriver driver = null;

            // then we build a JavascriptExecutor instance, where it'll manage and create the dynamic
            // object. 
            var jsExecutor = JavascriptExecutor.Create(new JavascriptRuntime((IJavaScriptExecutor)driver));

            // we start a new dynamic object of js. Now we can execute javascript through C#.
            // internally, it will parse the data and execute the script.
            dynamic js = jsExecutor.GetDynamicJs();

            // outputs a message to browser's console.
            // Execute() method is defined under MethodExecutor class.
            // Execute() shall run and not return anything.
            js.console.log("Hello world").Execute();

            // simple arithmetic operations
            // arithmetic operations are done on c# side.
            // TODO: Define either the developer wants it to be run on C# (slower) or on Javascript.
            // we define these values on browser.
            js.val1 = 10;
            js.val2 = 20;

            // it does the math of (val2 / 2) + val1 on c#.
            Int64 math = js.val1 + js.val2 / 2;

            // and then send the value to browser's console log
            js.console.log(math).Execute();

            // we can also define the value directly in javascript
            js.name = "John Doe";

            // and then send to javascript.
            // js.name will never be returned to C#, it will send directly the command to javascript
            // to execute the data (as javascript already has this member set).
            js.console.log(js.name).Execute();

            // now we are using the method substring of Javascript to get a part of the string defined
            // in javascript (js.name). It shall get only "John".
            // we have defined this in a local C# variable.
            // ExecuteReturn will execute the method and return its value.
            // TODO: Make it type-safe.
            string firstName = js.name.substring(0, 4).ExecuteReturn();

            // Now we output the value to C# and output to browser's output
            Console.WriteLine(firstName);
            js.console.log(firstName).Execute();

            // Done.
            Console.ReadKey();
        }
    }
}
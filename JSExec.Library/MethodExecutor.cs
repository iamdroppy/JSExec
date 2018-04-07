using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JSExec.Library
{
    internal class MethodExecutor
    {
        public static object Execute(JavascriptDynamic.JavascriptFallback fallback, InvokeMemberBinder binder, object[] args)
        {
            fallback.RuntimeJavascript.ExecuteJavascript(fallback.GetJavascriptCommand());
            return null;
        }

        public static object ExecuteReturn(JavascriptDynamic.JavascriptFallback fallback, InvokeMemberBinder binder, object[] args)
        {
            return fallback.RuntimeJavascript.ExecuteJavascriptReturn(fallback.GetJavascriptCommand());
        }
        public static object ToRaw(JavascriptDynamic.JavascriptFallback fallback, InvokeMemberBinder binder, object[] args)
        {
            return fallback.GetJavascriptCommand();
        }
    }

    internal class JavascriptInternals
    {
        public static bool TrySet(JavascriptDynamic.JavascriptFallback fallback, object value)
        {
            if (value.GetType() == typeof(JavascriptDynamic.JavascriptFallback))
            {
                var setValueFallback = (JavascriptDynamic.JavascriptFallback) value;
                fallback.RuntimeJavascript.SetJavascriptData(fallback.GetJavascriptCommand(), setValueFallback.GetJavascriptCommand());
            }
            else
            {
                fallback.RuntimeJavascript.SetJavascriptData(fallback.GetJavascriptCommand(), JavascriptArgumentUtils.Parse(value));
            }
            return true;
        }

        internal static object DoBinaryOperation(BinaryOperationBinder binder, JavascriptDynamic.JavascriptFallback fallback, object arg)
        {
            var runtimeJs = fallback.RuntimeJavascript;
            object firstObject = runtimeJs.ExecuteJavascriptReturn(fallback.GetJavascriptCommand());
            object secondObject = runtimeJs.ExecuteJavascriptReturn(arg.ToString());

            if (firstObject.GetType() == typeof(Int32))
                firstObject = (Int64)firstObject;
            if (secondObject.GetType() == typeof(Int32))
                secondObject = (Int64)secondObject;

            if (firstObject.GetType() == typeof(Int64) && secondObject.GetType() == typeof(Int64))
            {
                Int64 number1 = (Int64)firstObject;
                Int64 number2 = (Int64)secondObject;

                switch(binder.Operation)
                {
                    case ExpressionType.Add:
                        return number1 + number2;
                    case ExpressionType.Multiply:
                        return number1 * number2;
                    case ExpressionType.Divide:
                        return number1 / number2;
                    case ExpressionType.Subtract:
                        return number1 - number2;
                }
            }

            throw new Exception($"Invalid binary operation: {firstObject.GetType().Name} with {firstObject.GetType().Name}");
        }
    }
}

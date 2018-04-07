using JSExec.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSExec.Library
{
    internal class JavascriptDynamic : DynamicObject
    {
        /// <summary>
        /// Current IJavascriptRuntime instance
        /// </summary>
        private IJavascriptRuntime _runtimeJavascript;

        /// <summary>
        /// Creates a new instance of Javascript Dynamic
        /// </summary>
        /// <param name="runtimeJavascript">RunTime Javascript Instance</param>
        internal JavascriptDynamic(IJavascriptRuntime runtimeJavascript)
        {
            _runtimeJavascript = runtimeJavascript;
        }
        
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = JavascriptFallback.Create(_runtimeJavascript, binder, args);
            return true;
        }

        public class JavascriptFallback : JavascriptDynamic
        {
            private IJavascriptRuntime RuntimeJavascript { get; set; }
            private List<string> Command { get; set; }

            private JavascriptFallback(IJavascriptRuntime runtimeJavascript) : base(runtimeJavascript)
            {
                RuntimeJavascript = runtimeJavascript;
                Command = new List<string>();
            }

            public static JavascriptFallback Create(IJavascriptRuntime runtimeJavascript, InvokeMemberBinder binder, object[] args)
            {
                JavascriptFallback fallback = new JavascriptFallback(runtimeJavascript);
                fallback.AddCommand(binder, args);

                return fallback;
            }

            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                Console.WriteLine("Invoking from callback.");
                if (binder.Name == "Execute" && args.Length == 0)
                {
                    Console.WriteLine("Executing " + string.Join(".", Command));
                    result = null;
                    return true;
                }

                AddCommand(binder, args);
                result = this;
                return true;
            }

            private void AddCommand(InvokeMemberBinder binder, object[] args) => Command.Add(binder.Name + "(" + String.Join(", ", args) + ")");
        }
    }
}

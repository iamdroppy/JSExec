using JSExec.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
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

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = JavascriptFallback.CreateIndex(_runtimeJavascript, binder, indexes);
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = JavascriptFallback.CreateMember(_runtimeJavascript, binder);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            JavascriptFallback.CreateSetMember(_runtimeJavascript, binder, value);
            return true;
        }

        public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
        {
            result = JavascriptFallback.CreateBinaryOperation(_runtimeJavascript, binder, arg);
            return true;
        }

        public class JavascriptFallback : JavascriptDynamic
        {
            internal IJavascriptRuntime RuntimeJavascript { get; set; }
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

            public static JavascriptFallback CreateIndex(IJavascriptRuntime runtimeJavascript, GetIndexBinder binder, object[] indexes)
            {
                JavascriptFallback fallback = new JavascriptFallback(runtimeJavascript);
                fallback.AddIndex(binder, indexes);

                return fallback;
            }

            public static JavascriptFallback CreateMember(IJavascriptRuntime runtimeJavascript, GetMemberBinder binder)
            {
                JavascriptFallback fallback = new JavascriptFallback(runtimeJavascript);
                fallback.AddMember(binder);

                return fallback;
            }

            public static void CreateSetMember(IJavascriptRuntime runtimeJavascript, SetMemberBinder binder, object value)
            {
                JavascriptFallback fallback = new JavascriptFallback(runtimeJavascript);
                fallback.AddMember(binder);
                JavascriptInternals.TrySet(fallback, value);
            }

            public static object CreateBinaryOperation(IJavascriptRuntime runtimeJavascript, BinaryOperationBinder binder, object value)
            {
                JavascriptFallback fallback = new JavascriptFallback(runtimeJavascript);
                return JavascriptInternals.DoBinaryOperation(binder, fallback, value);
            }

            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                if (!TryInvokeCompiledMember(binder, args, out result))
                {
                    AddCommand(binder, args);
                    result = this;
                }

                return true;
            }

            private bool TryInvokeCompiledMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                result = null;
                var method = typeof(MethodExecutor).GetMethods().FirstOrDefault(s=>s.Name == binder.Name);

                if (method != null)
                {
                    result = method.Invoke(null, new object[] { this, binder, args });
                    return true;
                }

                return false;
            }

            public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
            {
                AddIndex(binder, indexes);
                result = this;
                return true;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                AddMember(binder);
                result = this;
                return true;
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                AddMember(binder);
                JavascriptInternals.TrySet(this, (JavascriptFallback)value);

                return true;
            }
            public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
            {
                result = JavascriptInternals.DoBinaryOperation(binder, this, arg);
                return true;
            }

            public override string ToString()
            {
                return GetJavascriptCommand();
            }

            internal string GetJavascriptCommand() => String.Join(".", Command);

            private void AddCommand(InvokeMemberBinder binder, object[] args) => Command.Add(binder.Name + "(" + JavascriptArgumentUtils.ParseArray(args) + ")");

            private void AddIndex(GetIndexBinder binder, object[] indexes) => Command[Command.Count() - 1] = Command.Last() + "[" + JavascriptArgumentUtils.ParseArray(indexes) + "]";

            private void AddMember(GetMemberBinder binder) => Command.Add(binder.Name);

            private void AddMember(SetMemberBinder binder) => Command.Add(binder.Name);
        }
    }
}

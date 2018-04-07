using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSExec.Library.Interfaces
{
    public interface IJavascriptRuntime
    {
        void ExecuteJavascript(string command);
        object ExecuteJavascriptReturn(string command);
        void SetJavascriptData(string data, object value);
    }
}

using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public static class JSExample
    {

        [JSInvokable]
        public static Task GetHelloMessage(string param)
        {
            var message = $"HELLO {param}";
            return Task.FromResult( message );
        }

    }
}

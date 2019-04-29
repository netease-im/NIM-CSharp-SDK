using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIM
{
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID

    [AttributeUsage(AttributeTargets.Method)]
    public class MonoPInvokeCallbackAttribute : AOT.MonoPInvokeCallbackAttribute
    {
        public MonoPInvokeCallbackAttribute(Type type)
            : base(type)
        {
        }
    }

#else
    /// <summary>
    /// Do nothing on windows desktop
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute(Type type)
        {

        }
    }
#endif
}

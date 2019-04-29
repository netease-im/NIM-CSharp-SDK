using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NimUtility
{
    public class Utilities
    {
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }

    public class DelegateBaton<T>
    {
        public object Data { get; set; }

        public T Action { get; set; }

        public IntPtr ToIntPtr()
        {
            var ptr = DelegateConverter.ConvertToIntPtr(this);
            return ptr;
        }

        public static DelegateBaton<T> FromIntPtr(IntPtr ptr)
        {
            var obj = DelegateConverter.ConvertFromIntPtr(ptr);
            var baton = obj as DelegateBaton<T>;
            return baton;
        }
    }

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID

    public class AotTypes : UnityEngine.MonoBehaviour
    {
        private static System.ComponentModel.TypeConverter _unused = new System.ComponentModel.TypeConverter();
    }
#endif

}

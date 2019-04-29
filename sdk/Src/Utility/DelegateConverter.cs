using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace NimUtility
{
    /// <summary>
    /// 转换 CLR Delegate 和 Native function pointer
    /// </summary>
    public static class DelegateConverter
    {
        private static readonly Dictionary<IntPtr, string> _allocedMemDic = new Dictionary<IntPtr, string>();

        public static IntPtr ConvertToIntPtr(Delegate d)
        {
            if (d != null)
            {
                GCHandle gch = GCHandle.Alloc(d);
                IntPtr ptr = GCHandle.ToIntPtr(gch);
                _allocedMemDic[ptr] = d.Method.Name;
                return ptr;
            }
            return IntPtr.Zero;
        }

        public static IntPtr ConvertToIntPtr(object obj)
        {
            if (obj != null)
            {
                GCHandle gch = GCHandle.Alloc(obj);
                IntPtr ptr = GCHandle.ToIntPtr(gch);
                _allocedMemDic[ptr] = obj.ToString();
                return ptr;
            }
            return IntPtr.Zero;
        }

        public static T ConvertFromIntPtr<T>(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return default(T);
            GCHandle handle = GCHandle.FromIntPtr(ptr);
            var x = (T)handle.Target;
            return x;
        }

        public static object ConvertFromIntPtr(IntPtr ptr)
        {
            return ConvertFromIntPtr<object>(ptr);
        }

        public static void Invoke<TDelegate>(this IntPtr ptr, params object[] args)
        {
            var d = ConvertFromIntPtr<TDelegate>(ptr);
            var delegateObj = d as Delegate;
            if (delegateObj != null)
            {
                System.Diagnostics.Debug.Assert(CheckDelegateParams(delegateObj, args));
                delegateObj.Method.Invoke(delegateObj.Target, args);
            }
        }

        public static void InvokeOnce<TDelegate>(this IntPtr ptr, params object[] args)
        {
            var d = ConvertFromIntPtr<TDelegate>(ptr);
            var delegateObj = d as Delegate;
            if (delegateObj != null)
            {
                System.Diagnostics.Debug.Assert(CheckDelegateParams(delegateObj, args));
                delegateObj.Method.Invoke(delegateObj.Target, args);
                FreeMem(ptr);
            }
        }

        static bool CheckDelegateParams(Delegate d, params object[] args)
        {
            var ps = d.Method.GetParameters();
            if (args == null)
                return true;
            if (args.Count() != ps.Count())
                return false;
            for (int i = 0; i < args.Count(); i++)
            {
                if (args[i] == null || ps[i].ParameterType.IsValueType)
                    continue;
                if (!ps[i].ParameterType.IsInstanceOfType(args[i]))
                    return false;
            }
            return true;
        }

        public static void FreeMem(this IntPtr ptr)
        {
            _allocedMemDic.Remove(ptr);
            GCHandle handle = GCHandle.FromIntPtr(ptr);
            handle.Free();
        }

        public static void ClearHandles()
        {
            foreach (var item in _allocedMemDic)
            {
                GCHandle handle = GCHandle.FromIntPtr(item.Key);
                handle.Free();
            }
            _allocedMemDic.Clear();
        }
    }

    /// <summary>
    /// IntPtr 的一些辅助操作,解决.NET framework 4.0以下不支持IntPtr add等操作 
    /// </summary>
    public static class IntPtrExtensions
    {
        #region Methods: Arithmetics
        public static IntPtr Decrement(this IntPtr pointer, Int32 value)
        {
            return Increment(pointer, -value);
        }

        public static IntPtr Decrement(this IntPtr pointer, Int64 value)
        {
            return Increment(pointer, -value);
        }

        public static IntPtr Decrement(this IntPtr pointer, IntPtr value)
        {
            switch (IntPtr.Size)
            {
                case sizeof(Int32):
                    return (new IntPtr(pointer.ToInt32() - value.ToInt32()));

                default:
                    return (new IntPtr(pointer.ToInt64() - value.ToInt64()));
            }
        }

        public static IntPtr Increment(this IntPtr pointer, Int32 value)
        {
            unchecked
            {
                switch (IntPtr.Size)
                {
                    case sizeof(Int32):
                        return (new IntPtr(pointer.ToInt32() + value));

                    default:
                        return (new IntPtr(pointer.ToInt64() + value));
                }
            }
        }

        public static IntPtr Increment(this IntPtr pointer, Int64 value)
        {
            unchecked
            {
                switch (IntPtr.Size)
                {
                    case sizeof(Int32):
                        return (new IntPtr((Int32)(pointer.ToInt32() + value)));

                    default:
                        return (new IntPtr(pointer.ToInt64() + value));
                }
            }
        }

        public static IntPtr Increment(this IntPtr pointer, IntPtr value)
        {
            unchecked
            {
                switch (IntPtr.Size)
                {
                    case sizeof(int):
                        return new IntPtr(pointer.ToInt32() + value.ToInt32());
                    default:
                        return new IntPtr(pointer.ToInt64() + value.ToInt64());
                }
            }
        }

        public static IntPtr CreateFromeIntPtr(this IntPtr pointer)
        {
            unchecked
            {
                switch (IntPtr.Size)
                {
                    case sizeof(int):
                        return new IntPtr(Marshal.ReadInt32(pointer));
                    default:
                        return new IntPtr(Marshal.ReadInt64(pointer));
                }
            }
        }

        #endregion
    }
}

using System;
using UnityEngine;

public static class ActionExtension
{
    public static void SafeInvoke(this Delegate action)
    {
        foreach (var d in action.GetInvocationList())
        {
            try
            {
                d.DynamicInvoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    public static void SafeInvoke<T>(this Delegate action, T arg)
    {
        foreach (var d in action.GetInvocationList())
        {
            try
            {
                d.DynamicInvoke(arg);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    public static void SafeInvoke<T1, T2>(this Delegate action, T1 arg1, T2 arg2)
    {
        foreach (var d in action.GetInvocationList())
        {
            try
            {
                d.DynamicInvoke(arg1, arg2);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    public static void SafeInvoke<T1, T2, T3>(this Delegate action, T1 arg1, T2 arg2, T3 arg3)
    {
        foreach (var d in action.GetInvocationList())
        {
            try
            {
                d.DynamicInvoke(arg1, arg2, arg3);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    public static void SafeInvoke<T1, T2, T3, T4>(this Delegate action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        foreach (var d in action.GetInvocationList())
        {
            try
            {
                d.DynamicInvoke(arg1, arg2, arg3, arg4);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    public static void SafeInvoke<T1, T2, T3, T4, T5>(this Delegate action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        foreach (var d in action.GetInvocationList())
        {
            try
            {
                d.DynamicInvoke(arg1, arg2, arg3, arg4, arg5);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
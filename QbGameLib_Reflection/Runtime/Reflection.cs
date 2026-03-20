using System;
using System.Reflection;

namespace QbGameLib.Reflection
{
    public static class Reflection
    {
        public static void InvokeIfExists<T>(T value, string name,  params object[] args)
        {
            Type type = typeof(T);
            MethodInfo method = type.GetMethod(name);
            if (method != null) method.Invoke(value, args);
        }
        
        public static void InjectIfExists<T>(T value, string name, object args)
        {
            Type type = typeof(T);
            BindingFlags flags = BindingFlags.NonPublic |  BindingFlags.Instance;
            FieldInfo field = type.GetField(name,flags);
            if (field != null && field.FieldType == args.GetType())
            {
                // if(type.IsValueType) field.SetValueDirect(__makeref(value), args);
                // else 
                    field.SetValue(value, args);
            }
        }
    }
}
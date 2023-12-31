﻿namespace Dawn.DMD.StarCruxExpansion.ReflectionHelpers;

using System.Linq.Expressions;
using System.Reflection.Emit;
using MonoMod.Utils;

internal static class PrivateFieldsHelper
{
    internal static Action<TField, TInstance> CreateFieldSetter<TField, TInstance>(this FieldInfo info)
    {
        var methodName = typeof(TInstance).FullName + ".set_" + info.Name;
        var setterMethod = new DynamicMethod(methodName, null, [typeof(TField), typeof(TInstance)], true);
        var gen = setterMethod.GetILGenerator();
        if (info.IsStatic)
        {
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stsfld, info);
        }
        else 
        {
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stfld, info);
        }
        gen.Emit(OpCodes.Ret);
        return setterMethod.CreateDelegate<Action<TField, TInstance>>();
    }
    
    internal static Func<TInstance, TParameter> CreateFieldGetter<TInstance, TParameter>(this FieldInfo field) 
        => (Func<TInstance, TParameter>) _CreateFieldGetterDelegate(typeof(TInstance), typeof(TParameter), field);
    
    // https://www.codeproject.com/Articles/412968/ReflectionHelper
    // Modified
    private static Delegate _CreateFieldGetterDelegate(Type instanceType, Type resultType, FieldInfo field)
    {
        
        Expression resultExpression;
        ParameterExpression parameter = null;
        Type funcType;

        if (field.IsStatic)
        {
            resultExpression = Expression.MakeMemberAccess(null, field);
            funcType = typeof(Func<>).MakeGenericType(resultType);
        }
        else
        {
            funcType = typeof(Func<,>).MakeGenericType(instanceType, resultType);

            parameter = Expression.Parameter(instanceType, "instance");
            
            Expression readParameter = parameter;

            if (field.DeclaringType != instanceType)
                if (field.DeclaringType != null)
                    readParameter = Expression.Convert(parameter, field.DeclaringType);

            resultExpression = Expression.MakeMemberAccess(readParameter, field);
        }

        if (field.FieldType != resultType)
            resultExpression = Expression.Convert(resultExpression, resultType);

        // Passing a nulled parameter into the Params array is not the same as passing null as the array.
        var lambda = parameter == null 
            ? Expression.Lambda(funcType, resultExpression) 
            : Expression.Lambda(funcType, resultExpression, parameter);

        var result = lambda.Compile();
        return result;
    }  
    
    public static Lazy<Func<TInstance, TReturnType>> CreateFieldGetterDelegate<TInstance, TReturnType>(string fieldName) where TInstance : class
    {
        var fieldInfo = typeof(TInstance).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (fieldInfo != null)
            return new Lazy<Func<TInstance, TReturnType>>(() => fieldInfo.CreateFieldGetter<TInstance, TReturnType>());
        
        
        ModLogger.LogError($"Unable to find field typeof({typeof(TInstance).Name}).{fieldName}");
        throw new NullReferenceException(nameof(fieldInfo));
    }
    
    public static Lazy<Action<TInstance, TReturnType>> CreateFieldSetterDelegate<TInstance, TReturnType>(string fieldName) where TInstance : class
    {
        var fieldInfo = typeof(TInstance).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (fieldInfo != null)
            return new Lazy<Action<TInstance, TReturnType>>(() => fieldInfo.CreateFieldSetter<TInstance, TReturnType>());
        
        
        ModLogger.LogError($"Unable to find field typeof({typeof(TInstance).Name}).{fieldName}");
        throw new NullReferenceException(nameof(fieldInfo));
    }
    
    
    internal static Func<TParameter> CreateStaticFieldGetter<TParameter>(this FieldInfo field, Type containingClass) 
        => (Func<TParameter>) _CreateFieldGetterDelegate(containingClass, typeof(TParameter), field);
    

    public static Lazy<Func<TReturnType>> CreateStaticFieldGetterDelegate<TReturnType>(Type containingClass, string fieldName)
    {
        var fieldInfo = containingClass.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);

        if (fieldInfo != null)
            return new Lazy<Func<TReturnType>>(() => fieldInfo.CreateStaticFieldGetter<TReturnType>(containingClass));
        
        
        ModLogger.LogError($"Unable to find field typeof({containingClass.Name}).{fieldName}");
        throw new NullReferenceException(nameof(fieldInfo));
    }
}
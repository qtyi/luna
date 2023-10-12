// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Luna.Compilers.Generators;

namespace Luna.Compilers.Tools;

internal static class Program
{
    public static int Main(string[] args)
    {
        try
        {
            Console.WriteLine($"Check override info of '{typeof(Microsoft.CodeAnalysis.CompilationOptions).FullName}':");
            foreach (var pair in GetBaseTypeOverrrideInfo())
            {
                (var param, var prop, var methodGroups) = pair.Value;
                if (methodGroups.IsDefaultOrEmpty)
                    Console.WriteLine($"    {param.Name} -> {prop.Name}");
                else
                {
                    foreach ((var method, var commonMethod) in methodGroups)
                    {
                        Console.WriteLine($"    {param.Name} -> {prop.Name} -> {method.Name}({method.GetParameters()[0].ParameterType}) -> {commonMethod.Name}({commonMethod.GetParameters()[0].ParameterType})");
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Generator cannot work due to: {0}.", e.Message);
            return 1;
        }

        return 0;
    }

    private static ImmutableDictionary<string, (ParameterInfo ctorParam, PropertyInfo prop, ImmutableArray<(MethodInfo method, MethodInfo commonMethod)> methodGroups)> GetBaseTypeOverrrideInfo()
    {
        var dicBuilder = ImmutableDictionary.CreateBuilder<string, (ParameterInfo ctorParam, PropertyInfo prop, ImmutableArray<(MethodInfo method, MethodInfo commonMethod)> methodGroups)>(StringComparer.OrdinalIgnoreCase);

        var baseType = typeof(Microsoft.CodeAnalysis.CompilationOptions);

        var constructor = baseType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).SingleOrDefault();
        Debug.Assert(constructor is not null, $"cannot find proper constructor in '{baseType.FullName}'");

        foreach (var param in constructor.GetParameters())
        {
            Debug.Assert(param.Name is not null);

            var prop = baseType.GetProperty(param.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
            Debug.Assert(prop is not null, $"cannot find corresponding property for constructor parameter '{param.Name}'");
            Debug.Assert(CanPossibleAssign(param.ParameterType, prop.PropertyType), $"cannot assign constructor parameter '{param}' to property '{prop.Name}'");

            if (prop.Name is "WarningLevel" or "CurrentLocalTime" or "DebugPlusMode" or "ReferencesSupersedeLowerVersions")
            {
                dicBuilder.Add(prop.Name, (param, prop, default));
                continue;
            };

            var methodName = prop.Name == "CheckOverflow" ? "WithOverflowChecks" : "With" + prop.Name;
            var methods = baseType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(mi => mi.Name == methodName).ToArray();
            Debug.Assert(methods.Length != 0, $"cannot find corresponding method 'With{prop.Name}' for option '{prop.Name}'");

            var arrBuilder = ImmutableArray.CreateBuilder<(MethodInfo method, MethodInfo commonMethod)>();
            foreach (var method in methods)
            {
                Debug.Assert(method.GetParameters().Length == 1, $"cannot process corresponding method '{method.Name}' (of {method.GetParameters().Length} parameters) for option '{prop.Name}'");
                Debug.Assert(CanPossibleAssign(method.GetParameters()[0].ParameterType, prop.PropertyType), $"cannot process corresponding method '{method.Name}' for option '{prop.Name}'");

                var commonMethodName = "CommonWith" + prop.Name;
                var commonMethod = baseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).SingleOrDefault(mi => mi.Name == commonMethodName && mi.GetParameters().Length == 1 && mi.GetParameters()[0].ParameterType == method.GetParameters()[0].ParameterType);
                Debug.Assert(commonMethod is not null, $"cannot find corresponding method '{commonMethod.Name}' for option '{prop.Name}'");

                arrBuilder.Add((method, commonMethod));
            }
            dicBuilder.Add(prop.Name, (param, prop, arrBuilder.ToImmutable()));
        }

        return dicBuilder.ToImmutable();

        static bool CanPossibleAssign(Type left, Type right)
        {
            if (left.IsAssignableFrom(right)) return true;

            return false;
        }
    }
}

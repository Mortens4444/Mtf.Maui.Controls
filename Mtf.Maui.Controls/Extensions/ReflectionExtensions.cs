using System.Reflection;

namespace Mtf.Maui.Controls.Extensions;

internal static class ReflectionExtensions
{
    public static List<T> CreateInstancesFromNamespace<T>(this string @namespace)
        where T : class
    {
        var result = new List<T>();

        if (String.IsNullOrWhiteSpace(@namespace))
        {
            return result;
        }

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var asm in assemblies)
        {
            Type[] types;
            try
            {
                types = asm.GetTypes();
            }
            catch (ReflectionTypeLoadException rex)
            {
                types = [.. rex.Types.Where(static t => t != null)];
            }

            foreach (var type in types)
            {
                if (type == null || !type.IsClass || type.IsAbstract || type.Namespace == null || !typeof(T).IsAssignableFrom(type))
                //if (type == null || !type.IsClass || type.IsAbstract || type.Namespace == null || !typeof(T).IsAssignableFrom(type) || type.GetConstructor(Type.EmptyTypes) == null)
                {
                    continue;
                }

                if (!type.Namespace.StartsWith(@namespace, StringComparison.Ordinal))
                {
                    continue;
                }

                try
                {
                    var ctor = type.GetConstructors()
                       .FirstOrDefault(c => c.GetParameters().All(p => p.HasDefaultValue));

                    if (ctor != null)
                    {
                        var args = ctor.GetParameters().Select(p => p.DefaultValue).ToArray();
                        var instance = ctor.Invoke(args);
                        result.Add((T)instance);
                    }
                    else
                    {
                        if (Activator.CreateInstance(type) is T obj)
                        {
                            result.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        return result;
    }

    public static List<T> CreateInstancesFromNamespace<T>(this Assembly assembly, string @namespace)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var result = new List<T>();

        var types = new Type[0];
        try { types = assembly.GetTypes(); }
        catch (ReflectionTypeLoadException rex) { types = [.. rex.Types.Where(t => t != null)]; }

        foreach (var type in types)
        {
            if (type == null || !type.IsClass || type.IsAbstract || type.Namespace == null || !type.Namespace.StartsWith(@namespace, StringComparison.Ordinal) || !typeof(T).IsAssignableFrom(type))
            {
                continue;
            }

            try
            {
                var ctor = type.GetConstructors()
                       .FirstOrDefault(c => c.GetParameters().All(p => p.HasDefaultValue));

                if (ctor != null)
                {
                    var args = ctor.GetParameters().Select(p => p.DefaultValue).ToArray();
                    var instance = ctor.Invoke(args);
                    result.Add((T)instance);
                }
                else
                {
                    if (Activator.CreateInstance(type) is T obj)
                    {
                        result.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        return result;
    }
}

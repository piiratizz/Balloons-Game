using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(T service)
    {
        _services[typeof(T)] = service;
    }

    public static T Get<T>()
    {
        if (_services.TryGetValue(typeof(T), out var service))
            return (T)service;

        throw new Exception($"Service of type {typeof(T)} is not registered");
    }

    public static void Unregister<T>()
    {
        _services.Remove(typeof(T));
    }

    public static void Clear()
    {
        _services.Clear();
    }
}
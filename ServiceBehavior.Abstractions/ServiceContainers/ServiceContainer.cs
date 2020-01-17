using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceBehavior.Abstractions
{
    public class ServiceContainer : IServiceContainer
    {
        private IDictionary<string, object> _dictionary = new Dictionary<string, object>();

        public void RegisterService(object service)
        {
            string key = service.GetType().GetInterfaces()
                .OfType<TypeInfo>()
                .Where(t => t.ImplementedInterfaces.Contains(typeof(IServiceBehavior)))
                .Select(t => GetKey(t))
                .FirstOrDefault();

            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException($"Could not find an interface that implements IServiceBehavior in {service.GetType()}.");
            }

            RegisterService(key, service);
        }

        public void RegisterService(string key, object service)
        {
            if (_dictionary.ContainsKey(key))
            {
                _dictionary.Remove(key);
            }

            _dictionary.Add(key, service);
        }

        public T GetService<T>() where T : class
        {
            string key = GetKey(typeof(T));
            return GetService<T>(key);
        }

        public T GetService<T>(string key) where T : class
        {
            if (_dictionary.TryGetValue(key, out object service))
            {
                return (T)service;
            }

            return null;
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        private string GetKey(Type type)
        {
            return type.FullName;
        }
    }
}

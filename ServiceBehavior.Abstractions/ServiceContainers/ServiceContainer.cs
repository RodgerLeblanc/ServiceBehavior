using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceBehavior.Abstractions
{
    public class ServiceContainer : IServiceContainer
    {
        private static readonly Assembly _assembly;
        private IDictionary<string, object> _dictionary = new Dictionary<string, object>();

        static ServiceContainer()
        {
            _assembly = typeof(ServiceContainer).Assembly;
        }

        public void RegisterService(object service)
        {
            string key = service.GetType().GetInterfaces()
                .Where(t => t.Assembly == _assembly)
                .Select(t => GetKey(t))
                .FirstOrDefault();

            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("The service must register its own interface.");
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

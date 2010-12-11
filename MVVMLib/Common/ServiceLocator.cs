using System;
using System.Collections.Generic;

namespace MVVMLib.Common
{
    public class ServiceLocator : IServiceProvider
    {
        private Dictionary<Type,Object> services = new Dictionary<Type, object>();

        public T GetService<T>()
        {
            return (T) GetService(typeof (T));
        }

        public object GetService(Type serviceType)
        {
            lock (services)
            {
                if (services.ContainsKey(serviceType))
                    return services[serviceType];
            }
            return null;
        }


        public bool RegisterService<T>(T service, bool overwriteExisting)
        {
            lock (services)
            {
                if(!services.ContainsKey(typeof(T)))
                {
                    services.Add(typeof(T),service);
                    return true;
                }
                else if(overwriteExisting)
                {
                    services[typeof (T)] = service;
                    return true;
                }
            }
            return false;
        }

        public bool RegisterService<T>(T service)
        {
            return RegisterService(service, true);
        }

    }
}
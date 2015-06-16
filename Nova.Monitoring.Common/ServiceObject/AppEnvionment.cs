using System;
using System.Collections.Generic;
using System.Linq;

namespace Nova.Monitoring.Common
{
    public class AppEnvionment : IServiceProvider
    {

        public IDataService ServiceProxy { get; set; }


        private Dictionary<Type, object> serviceMap;

        private AppEnvionment()
        {
            serviceMap = new Dictionary<Type, object>();
        }

        private static AppEnvionment _current = new AppEnvionment();

        public static AppEnvionment Current
        {
            get
            {
                return _current;
            }
        }

        public object GetService(Type serviceType)
        {
            if (serviceMap.ContainsKey(serviceType))
            {
                return serviceMap[serviceType];
            }
            else
            {
                throw new Exception("指定的服务没有找到。");
            }
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public void AddService(Type type, object service)
        {
            var trueType = service.GetType();

            if (type != trueType && !trueType.IsSubclassOf(type) && !trueType.GetInterfaces().Contains(type))
            {
                throw new Exception("服务不能被转换为指定的类型。");
            }
            if (serviceMap.ContainsKey(type))
            {
                serviceMap[type] = service;
            }
            else
            {
                serviceMap.Add(type, service);
            }
        }
    }
}

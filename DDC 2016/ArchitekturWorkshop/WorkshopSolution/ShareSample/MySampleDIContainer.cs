using System;
using System.Collections.Generic;

namespace ShareSample
{
    class MySampleDIContainer
    {
        private Dictionary<Type, Type> mapping = new Dictionary<Type, Type>();
        public void RegisterType(Type @interface, Type implementation)
        {
            mapping.Add(@interface, implementation);
        }

        public object Resolve(Type type)
        {
            var targetType = mapping[type];
            var targetCtor = targetType.GetConstructors()[0];
            List<object> parameterInstances = new List<object>();
            foreach (var parameter in targetCtor.GetParameters())
            {
                var parameterInstance = Resolve(parameter.ParameterType);
                parameterInstances.Add(parameterInstance);
            }

            return Activator.CreateInstance(targetType, parameterInstances.ToArray());
        }
    }
}
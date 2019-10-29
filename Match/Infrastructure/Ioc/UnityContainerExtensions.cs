using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Unity;

namespace Match.Infrastructure
{
    public static class UnityContainerExtensions
    {
        public static void RegisterInheritedTypes(this Unity.IUnityContainer container, Assembly assembly, System.Type baseType)
        {
            var allTypes = assembly.GetTypes();
            var baseInterfaces = baseType.GetInterfaces();

            foreach (var type in allTypes)
            {
                var test = type.BaseType;
                if (type.BaseType != null && type.BaseType.GenericEq(baseType))
                {
                    var typeInterface = type.GetInterfaces().FirstOrDefault(x => !baseInterfaces.Any(bi => bi.GenericEq(x)));
                    if (typeInterface == null)
                    {
                        continue;
                    }
                    container.RegisterType(typeInterface, type);
                    //container.RegisterSingleton(typeInterface, type);
                }
            }

            var ok = assembly.GetTypes();
        }
    }

    public static class TypeExtensions
    {
        public static bool GenericEq(this Type type, Type toCompare)
        {
            return type.Namespace == toCompare.Namespace && type.Name == toCompare.Name;
        }

    }
}

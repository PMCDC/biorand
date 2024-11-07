using biorand.app.Attributes.DependencyInjectionAttributes;
using Prism.Ioc;
using System;
using System.Linq;
using System.Reflection;

namespace biorand.app.Extensions
{
    public static class ContainerRegistryExtension
    {
        /// <summary>
        /// Automaticaly register all dependencies of the Exsphere solution.
        /// </summary>
        /// <param name="containerRegistry"></param>
        public static void RegisterDependencies(this IContainerRegistry containerRegistry)
        {
            PreLoadProjectsAssemblies();
            RegisterSingletons(containerRegistry);
        }

        /// <summary>
        /// Register all class implementations from interfaces that have the <b>RegisterSingletonAttribute</b>.
        /// </summary>
        /// <param name="containerRegistry"></param>
        private static void RegisterSingletons(IContainerRegistry containerRegistry)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
            var interfaces = types.Where(type => type.IsInterface && type.GetCustomAttribute(typeof(RegisterSingletonAttribute)) != null).ToList();
            foreach (var interfaceType in interfaces)
            {
                var implementations = types.Where(type => !type.IsInterface && type.IsClass && interfaceType.IsAssignableFrom(type)).ToList();
                foreach (var implementationType in implementations)
                {
                    containerRegistry.RegisterSingleton(interfaceType, implementationType);
                    System.Diagnostics.Debug.WriteLine($"Automatic Dependency Injection Registration: Implementation class '{implementationType.Name}' of interface '{interfaceType.Name}' was registered to the ContainerRegistry as a Singleton.");
                }
            }
        }

        /// <summary>
        /// Pre-load assemblies of all projects related to this solution.
        /// </summary>
        private static void PreLoadProjectsAssemblies()
        {
            System.Diagnostics.Debug.WriteLine($"Automatic Dependency Injection Registration: Loading assemblies...");
            //Assembly.LoadFrom("biorand.desktop.Core.dll");
        }
    }
}



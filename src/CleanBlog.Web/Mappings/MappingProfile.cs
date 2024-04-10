using AutoMapper;
using CleanBlog.Application.Abstractions;
using System.Reflection;

namespace CleanBlog.Web.Mappings
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var assemblyList = new List<Assembly>() { assembly };
            assemblyList.AddRange(assembly.GetReferencedAssemblies()
                .Where(a => a.FullName.StartsWith(nameof(CleanBlog)))
                .Select(x => Assembly.Load(x)));

            List<Type> types = new List<Type>();
            foreach (var item in assemblyList)
            {
                types.AddRange(item.GetExportedTypes()
                    .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
                    .ToList());
            }

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping");
                if (methodInfo is not null)
                {
                    methodInfo?.Invoke(instance, new object[] { this });
                }

                var mappingInterfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>));
                foreach (var interfaceItem in mappingInterfaces)
                {
                    interfaceItem.GetMethod("Mapping").Invoke(instance, new object[] { this });
                }
            }
        }
    }
}

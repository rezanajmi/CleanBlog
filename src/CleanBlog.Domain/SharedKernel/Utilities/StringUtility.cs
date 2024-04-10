using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Domain.SharedKernel.Utilities
{
    public static class StringUtility
    {
        public static string GenerateChacheKeyWithObject(object obj)
        {
            var useCache = obj as IUseCache;
            var keyInfixes = string.Empty;
            foreach (var item in useCache.Infixes)
            {
                keyInfixes += $"{item}&";
            }
            keyInfixes = keyInfixes.Remove(keyInfixes.Length - 1);

            string key = $"{obj.GetType().Name}#{keyInfixes}#";

            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.Name == nameof(IUseCache.Infixes)) continue;
                key += $"{prop.Name}:{prop.GetValue(obj)}&";
            }
            return key;
        }

        public static string GenerateKeyWithParams(string key, string[] parameters)
        {
            if (parameters == null)
            {
                return key;
            }

            var complexKey = key;

            foreach (var param in parameters)
            {
                complexKey += $"&{param}";
            }

            return complexKey;
        }
    }
}

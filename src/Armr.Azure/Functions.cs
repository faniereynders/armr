using System;

namespace Armr.Azure
{
    public static class Functions
    {
        public static DynamicString ResourceId<T>(object name) where T: IResourceType
        {
            var resource = Activator.CreateInstance<T>();
            return new DynamicString($"resourceId('{ resource.Type }', {name})");
        }
        public static DynamicString Variables(string name) => 
            new DynamicString($"variables('{ name }')");
        public static DynamicString Parameters(string name) =>
             new DynamicString($"parameters('{ name }')");

        public static DynamicString Concat(params object[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetType() == typeof(string))
                {
                    items[i] = $"'{items[i]}'";
                }
            }
            var values = String.Join(", ", items);
            return new DynamicString($"{nameof(Concat).ToLower()}({ values })");
        }
    }
}

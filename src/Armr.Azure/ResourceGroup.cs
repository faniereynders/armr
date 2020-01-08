namespace Armr.Azure
{
    public static class ResourceGroup
    {
        public static DynamicString Location => new DynamicString("resourceGroup().location");
    }
}

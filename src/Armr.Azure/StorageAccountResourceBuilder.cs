namespace Armr.Azure
{
    public class StorageAccountResourceBuilder:ResourceBuilder<StorageAccount,StorageAccountResourceBuilder> 
    {
        private readonly StorageAccount storageAccount;

        public StorageAccountResourceBuilder()
        {
            storageAccount = new StorageAccount();

        }

        public StorageAccountResourceBuilder StorageAccountFeature()
        {
            return this;
        }

    }
}